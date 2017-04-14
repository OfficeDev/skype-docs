using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Microsoft.SfB.PlatformService.SDK.Common
{
    #region CacheItem

    /// <summary>
    /// CacheItem
    /// </summary>
    /// <typeparam name="TCacheItem">The type of the cache item.</typeparam>
    internal class CacheItem<TCacheItem>
    {
        #region contructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheItem{T}"/> class.
        /// </summary>
        /// <param name="item">The item.</param>
        public CacheItem(TCacheItem item)
        {
            if (EqualityComparer<TCacheItem>.Default.Equals(item, default(TCacheItem)))
            {
                throw new ArgumentNullException(nameof(item));
            }
            Item = item;
            LastAccessTime = DateTime.UtcNow;
        }

        #endregion

        #region internal properties

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <value>
        /// The item.
        /// </value>
        internal TCacheItem Item { get; }

        /// <summary>
        /// Gets or sets the last access time.
        /// </summary>
        /// <value>
        /// The last access time.
        /// </value>
        internal DateTime LastAccessTime { get; set; }

        #endregion
    }

    #endregion

    #region LeastRecentlyUsedCacheSettings

    /// <summary>
    /// LeastRecentlyUsedCacheSettings
    /// </summary>
    public class LeastRecentlyUsedCacheSettings
    {
        #region public properties

        /// <summary>
        /// Gets or sets the low water mark.
        /// </summary>
        /// <value>
        /// The low water mark.
        /// </value>
        public int LowWaterMark { get; set; }

        /// <summary>
        /// Gets or sets the high water mark.
        /// </summary>
        /// <value>
        /// The high water mark.
        /// </value>
        public int HighWaterMark { get; set; }

        /// <summary>
        /// Gets or sets the staleness.
        /// </summary>
        /// <value>
        /// The staleness.
        /// </value>
        public TimeSpan Staleness { get; set; }

        /// <summary>
        /// Gets or sets whether the access time needs to be updated for GETs.
        /// </summary>
        /// <value>
        /// Whether the access time needs to be updated for GETs.
        /// </value>
        public bool? UpdateAccessTimesOnGets { get; set; }

        #endregion
    }

    #endregion

    #region LeastRecentlyUsedCache

    /// <summary>
    /// LeastRecentlyUsedCache
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    /// <remarks>
    /// A generic cache which cleans up items based on least-recently-used with staleness of more than 30 minutes.
    /// The caller can specify a LowWaterMark and a HighWaterMark. We use these watermarks to optimize
    /// our cleanup mechanism.
    ///
    /// The cleanup will be triggered if both the following 2 conditions are satisfied :
    /// 1) An item is being added to the cache
    /// 2) The current count of the cache is more than the HighWaterMark
    ///
    /// We will cleanup all the items in the cache that are stale.
    ///
    /// If even after the cleanup :
    /// 1) The number of items in the cache are more than the LowWaterMark
    ///     a) We will change the LowWaterMark to the value of highwatermark and increase the highwatermark
    ///     b) We do this to avoid triggering the cleanup again and again (optimization of the O(N) operation)
    ///        if the cache is growing fast
    /// </remarks>
    public class LeastRecentlyUsedCache<TKey, TItem>
    {
        #region private variables

        /// <summary>
        /// The initia size
        /// </summary>
        private const int INITIAL_SIZE = 50;

        /// <summary>
        /// The high water mark
        /// </summary>
        private int m_highWaterMark = 40;

        /// <summary>
        /// The low water mark
        /// </summary>
        private int m_lowWaterMark = 30;

        /// <summary>
        /// The inner cache
        /// </summary>
        private readonly Dictionary<TKey, CacheItem<TItem>> m_innerCache = new Dictionary<TKey, CacheItem<TItem>>(INITIAL_SIZE + 10);

        /// <summary>
        /// Represents whether the access time needs to be updated for GETs.
        /// </summary>
        private readonly bool m_updateAccessTimeOnGets = true;

        #endregion

        #region constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LeastRecentlyUsedCache{TKey, TItem}"/> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <exception cref="System.ArgumentException">High water mark value should be greater than or equal to low water mark setting.</exception>
        public LeastRecentlyUsedCache(LeastRecentlyUsedCacheSettings settings = null)
        {
            if (settings != null)
            {
                if (settings.HighWaterMark < settings.LowWaterMark)
                {
                    throw new ArgumentException("High water mark value should be greater than or equal to low water mark setting.");
                }

                m_highWaterMark = settings.HighWaterMark;
                m_lowWaterMark = settings.LowWaterMark;

                if (settings.Staleness != default(TimeSpan))
                {
                    MinStaleness = settings.Staleness;
                }

                if (settings.UpdateAccessTimesOnGets.HasValue)
                {
                    m_updateAccessTimeOnGets = settings.UpdateAccessTimesOnGets.Value;
                }
            }
        }

        #endregion

        #region internal properties

        /// <summary>
        /// Gets the high water mark.
        /// </summary>
        /// <value>
        /// The high water mark.
        /// </value>
        internal int HighWaterMark
        {
            get { return m_highWaterMark; }
        }

        /// <summary>
        /// Gets the low water mark.
        /// </summary>
        /// <value>
        /// The low water mark.
        /// </value>
        internal int LowWaterMark
        {
            get { return m_lowWaterMark; }
        }

        #endregion

        #region Private properties

        /// <summary>
        /// Gets the minimum staleness.
        /// </summary>
        /// <value>
        /// The minimum staleness.
        /// </value>
        private TimeSpan MinStaleness { get; } = TimeSpan.FromMinutes(30);

        #endregion

        #region public methods

        /// <summary>
        /// Gets or creates the cache item.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="itemCreator">The item creator.</param>
        /// <returns></returns>
        public TItem GetOrCreate(TKey key, Func<TItem> itemCreator)
        {
            if (itemCreator == null)
            {
                throw new ArgumentNullException(nameof(itemCreator));
            }

            if (EqualityComparer<TKey>.Default.Equals(key, default(TKey)))
            {
                throw new ArgumentNullException(nameof(key));
            }

            CacheItem<TItem> cachedItem = null;
            lock (m_innerCache)
            {
                bool createItem = false;
                if (m_innerCache.TryGetValue(key, out cachedItem))
                {
                    var now = DateTime.UtcNow;
                    if ((now - cachedItem.LastAccessTime) >= this.MinStaleness)
                    {
                        createItem = true;
                    }
                    else
                    {
                        if (m_updateAccessTimeOnGets)
                        {
                            cachedItem.LastAccessTime = DateTime.UtcNow;
                        }
                    }
                }
                else
                {
                    createItem = true;
                }

                if (createItem)
                {
                    // Need to check if we have space to add an item. If not we need to clear some space.
                    this.CleanupIfNecessary();

                    var item = itemCreator();
                    Debug.Assert(!EqualityComparer<TItem>.Default.Equals(item, default(TItem)), "Item is null");

                    cachedItem = new CacheItem<TItem>(item);
                    cachedItem.LastAccessTime = DateTime.UtcNow;
                    m_innerCache[key] = cachedItem;
                }
            }

            return cachedItem.Item;
        }

        /// <summary>
        /// Tries to get the item in cache if present.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public bool TryGet(TKey key, out TItem item)
        {
            if (EqualityComparer<TKey>.Default.Equals(key, default(TKey)))
            {
                throw new ArgumentNullException(nameof(key));
            }

            item = default(TItem);
            CacheItem<TItem> cachedItem = null;
            bool isPresentInCache = false;
            lock (m_innerCache)
            {
                if (m_innerCache.TryGetValue(key, out cachedItem))
                {
                    cachedItem.LastAccessTime = DateTime.UtcNow;
                    item = cachedItem.Item;
                    isPresentInCache = true;
                }
            }

            return isPresentInCache;
        }

        #endregion

        #region private methods

        /// <summary>
        /// Cleanups if necessary.
        /// </summary>
        private void CleanupIfNecessary()
        {
            lock (m_innerCache)
            {
                if (m_innerCache.Count >= this.HighWaterMark)
                {
                    DateTime now = DateTime.UtcNow;
                    List<TKey> keysToRemove = new List<TKey>();

                    // Get all the stale items and remove them.
                    foreach (var kv in m_innerCache)
                    {
                        var val = kv.Value;
                        if ((now - val.LastAccessTime) > this.MinStaleness)
                        {
                            // Item is stale. We need remove.
                            keysToRemove.Add(kv.Key);
                        }
                    }

                    // Remove all stale keys.
                    foreach (var key in keysToRemove)
                    {
                        m_innerCache.Remove(key);
                    }

                    // Check if we have reached low water mark.
                    if (m_innerCache.Count >= this.LowWaterMark)
                    {
                        int existingLowWaterMark = m_lowWaterMark;
                        m_lowWaterMark = m_highWaterMark;
                        m_highWaterMark += existingLowWaterMark;
                    }
                }
            }
        }

        #endregion

    }

    #endregion

}
