using EPiServer.ServiceLocation;

namespace ExtendedContentCopy
{
    /// <summary>
    /// Configuration for extended copy addon
    /// </summary>
    [Options]
    public class ExtendedContentCopyOptions
    {
        /// <summary>
        /// Determinates how plugin should bahave. Default <see langword="ExtendedContentCopyMode.Command"/>
        /// </summary>
        public ExtendedContentCopyMode Mode { get; set; } = ExtendedContentCopyMode.Command;

        /// <summary>
        /// When true, then addon is active. Default <see langword="true"/>
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// Allows to configure available paste actions
        /// </summary>
        public AllowedActions AllowedPasteActions { get; private set; } = new AllowedActions();

        /// <summary>
        /// Allows to configure default action values
        /// </summary>
        public Defaults PasteDefaults { get; private set; } = new Defaults();

        public class AllowedActions
        {
            public bool PublishOnDestination { get; set; } = true;

            public bool CopyAllLanguageBranches { get; set; } = true;

            public bool CopyDescendants { get; set; } = true;
        }

        public class Defaults
        {
            public bool PublishOnDestination { get; set; } = false;

            public bool CopyAllLanguageBranches { get; set; } = true;

            public bool CopyDescendants { get; set; } = true;
        }

        public enum ExtendedContentCopyMode
        {
            /// <summary>
            /// Plugin is off
            /// </summary>
            Off,

            /// <summary>
            /// For extendd content paste we are using command
            /// </summary>
            Command,

            /// <summary>
            /// Command is not available and default paste behaviour is overriden
            /// </summary>
            Auto
        }
    }

}