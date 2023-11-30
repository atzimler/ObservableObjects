using System;

namespace ATZ.ObservableObjects
{
    /// <summary>
    /// Resource locking object to suspend event signaling.
    /// </summary>
    public class SuspendPropertyChangedEvent : IDisposable
    {
        /// <summary>
        /// Delegate type for action to resume event handling.
        /// </summary>
        public delegate void ResumeEvent();

        // TODO: [NotNull]
        private readonly ResumeEvent _resumeEvent;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="resumeEvent">The delegate to resume event.</param>
        internal SuspendPropertyChangedEvent(
            // TODO: [NotNull] 
            ResumeEvent resumeEvent)
        {
            _resumeEvent = resumeEvent;
        }

        /// <summary>
        /// Dispose the event suspension object.
        /// </summary>
        public void Dispose()
        {
            _resumeEvent();
        }
    }
}
