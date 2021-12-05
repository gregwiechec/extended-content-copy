namespace ExtendedContentCopy
{
    /// <summary>
    /// defines how paste should work
    /// </summary>
    public class PasteMode
    {
        public bool? PublishOnDestination { get; set; }

        public bool? CopyAllLanguageBranches { get; set; }

        public bool? CopyDescendants { get; set; }

        public PasteMode()
        {
        }

        public PasteMode(ExtendedContentCopyOptions options)
        {
            PublishOnDestination = options.PasteDefaults.PublishOnDestination;
            CopyAllLanguageBranches = options.PasteDefaults.CopyAllLanguageBranches;
            CopyDescendants = options.PasteDefaults.CopyDescendants;
        }

        public PasteMode(PasteMode other)
        {
            PublishOnDestination = other.PublishOnDestination;
            CopyAllLanguageBranches = other.CopyAllLanguageBranches;
            CopyDescendants = other.CopyDescendants;
        }
    }
}