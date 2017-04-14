using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.SfB.PlatformService.SDK.Common;
using Microsoft.Rtc.Internal.Platform.ResourceContract;

namespace Microsoft.SfB.PlatformService.SDK.ClientModel
{
    /// <summary>
    /// The base class of <see cref="MessagingCall"/> and <see cref="AudioVideoCall"/>>.
    /// </summary>
    /// <typeparam name="TPlatformResource">The type of the t platform resource.</typeparam>
    /// <typeparam name="TInvitation">The type of the t invitation.</typeparam>
    /// <typeparam name="TCapabilities">The type of the t capabilities.</typeparam>
    /// <seealso cref="Microsoft.SfB.PlatformService.SDK.ClientModel.BasePlatformResource{TPlatformResource, TCapabilities}" />
    /// <seealso cref="Microsoft.SfB.PlatformService.SDK.ClientModel.ICall{TInvitation}" />
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

        /// <summary>
        /// Establishes the Call
        /// </summary>
        /// <param name="loggingContext">The logging context.</param>
        /// <returns>Task&lt;TInvitation&gt;.</returns>
        public abstract Task<TInvitation> EstablishAsync(LoggingContext loggingContext);

        /// <summary>
        /// Terminates the Call
        /// </summary>
        /// <param name="loggingContext">The logging context.</param>
        /// <returns>Task.</returns>
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

    /// <summary>
    /// The Arguments for the event when the CallState is changed
    /// </summary>
    /// <seealso cref="System.EventArgs" />
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

        /// <summary>
        /// Gets the old state.
        /// </summary>
        /// <value>The old state.</value>
        public CallState OldState { get; }

        /// <summary>
        /// Gets the new state.
        /// </summary>
        /// <value>The new state.</value>
        public CallState NewState { get; }

        #endregion
    }
}
