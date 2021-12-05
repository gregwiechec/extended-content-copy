namespace ExtendedContentCopy
{
    /// <summary>
    /// merge user selected values with predefined and default values
    /// </summary>
    internal static class PredefinedOptionsMerger
    {
        public static PasteMode Merge(PasteMode userPasteMode, ExtendedContentCopyOptions options)
        {
            var pasteMode = new PasteMode(userPasteMode);

            if (options.AllowedPasteActions.PublishOnDestination)
            {
                if (!pasteMode.PublishOnDestination.HasValue)
                {
                    pasteMode.PublishOnDestination = options.PasteDefaults.PublishOnDestination;
                }
            }
            else
            {
                pasteMode.PublishOnDestination = null;
            }

            if (options.AllowedPasteActions.CopyAllLanguageBranches)
            {
                if (!pasteMode.CopyAllLanguageBranches.HasValue)
                {
                    pasteMode.CopyAllLanguageBranches = options.PasteDefaults.CopyAllLanguageBranches;
                }
            }
            else
            {
                pasteMode.CopyAllLanguageBranches = null;
            }

            if (options.AllowedPasteActions.CopyDescendants)
            {
                if (!pasteMode.CopyDescendants.HasValue)
                {
                    pasteMode.CopyDescendants = options.PasteDefaults.CopyDescendants;
                }
            }
            else
            {
                pasteMode.CopyDescendants = null;
            }

            return pasteMode;
        }
    }
}