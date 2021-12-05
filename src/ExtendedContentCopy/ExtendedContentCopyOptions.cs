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
        /// When true, then addon is active. Default <see langword="true"/>
        /// </summary>
        public bool Enabled { get; set; } = true;

        public AllowedActions AllowedPasteActions { get; private set; } = new AllowedActions();

        public Defaults PasteDefaults { get; private set; } = new Defaults();

        /// <summary>
        /// edit mode command configuration
        /// </summary>
        public CommandOptions Command { get; private set; } = new CommandOptions();

        public class AllowedActions
        {
            public bool PublishOnDestination { get; set; } = true;

            public bool CopyAllLanguageBranches { get; set; } = true;

            public bool CopyDescendants { get; set; } = true;
        }

        public class Defaults
        {
            public bool PublishOnDestination { get; set; } = true;

            public bool CopyAllLanguageBranches { get; set; } = true;

            public bool CopyDescendants { get; set; } = true;
        }

        public class CommandOptions
        {
            public bool Enabled { get; set; } = true;

            public bool PublishOnDestination { get; set; } = true;

            public bool CopyAllLanguageBranches { get; set; } = true;

            public bool CopyDescendants { get; set; } = true;
        }
    }

}