using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.SfB.PlatformService.SDK.Common;
using Microsoft.Rtc.Internal.Platform.ResourceContract;

namespace Microsoft.SfB.PlatformService.SDK.ClientModel
{
    internal abstract class Call<TPlatformResource, TInvitation, TCapabilities>
        : BasePlatformResource<TPlatformResource, TCapabilities>, ICall<TInvitation>
        where TPlatformResource : CallResource
    {
        #region Private fields

        private EventHandler<CallStateChangedEventArgs> m_callStateChanged;

        #endregion

        #region Constructor

        internal Call(IRestfulClient restfulClient, TPlatformResource resource, Uri baseUri, Uri resourceUri, object parent)
            : base(restfulClient, resource, baseUri, resourceUri, parent)
        {
        }

        #endregion

        #region Public properties

        /// <summary>
        /// The call state
        /// </summary>
        public CallState State
        {
            get { return PlatformResource?.State ?? CallState.Disconnected; }
        }

        #endregion

        #region Public events

        /// <summary>
        /// Event raised when <see cref="CallState"/> changes for this Call.
        /// </summary>
        /// <remarks>
        /// This event is raised <i>after</i> the corresponding
        /// <see cref="BasePlatformResource{TPlatformResource, TCapabilities}.HandleResourceUpdated"/>,
        /// <see cref="BasePlatformResource{TPlatformResource, TCapabilities}.HandleResourceCompleted"/> or
        /// <see cref="BasePlatformResource{TPlatformResource, TCapabilities}.HandleResourceRemoved"/>
        /// events have been raised.
        /// </remarks>
        public event EventHandler<CallStateChangedEventArgs> CallStateChanged
        {
            add
            {
                if (m_callStateChanged == null || !m_callStateChanged.GetInvocationList().Contains(value))
                {
                    m_callStateChanged += value;
                }
            }
            remove { m_callStateChanged -= value; }
        }

        #endregion

        #region Public methods

        public abstract Task<TInvitation> EstablishAsync(LoggingContext loggingContext);

        public abstract Task TerminateAsync(LoggingContext loggingContext);

        #endregion

        #region Internal methods

        internal override void HandleResourceEvent(EventContext eventcontext)
        {
            CallState oldState = State;

            // Raise resource events before CallStateChanged event
            base.HandleResourceEvent(eventcontext);

            CallState newState = State;

            if(oldState != newState)
            {
                m_callStateChanged?.Invoke(this, new CallStateChangedEventArgs(oldState, State));
            }
        }

        #endregion
    }

    public class CallStateChangedEventArgs : EventArgs
    {
        #region Consructor

        internal CallStateChangedEventArgs(CallState oldState, CallState newState)
        {
            OldState = oldState;
            NewState = newState;
        }

        #endregion

        #region Public properties

        public CallState OldState { get; }

        public CallState NewState { get; }

        #endregion
    }
}
