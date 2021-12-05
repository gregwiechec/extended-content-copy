using Xunit;

namespace ExtendedContentCopy.Tests
{
    [Trait(nameof(PredefinedOptionsMerger), nameof(PredefinedOptionsMerger.MergeWithAvailableOptions))]
    public class PredefinedOptionsMergerTests
    {
        private PasteMode pasteMode;
        private ExtendedContentCopyOptions options;

        public PredefinedOptionsMergerTests()
        {
            pasteMode = new PasteMode();
            options = new ExtendedContentCopyOptions();
        }

        public class When_controlling_Publish_is_allowed: PredefinedOptionsMergerTests
        {
            public When_controlling_Publish_is_allowed()
            {
                options.AllowedPasteActions.PublishOnDestination = true;
            }

            public class And_PublishOnDestination_is_NOT_set: When_controlling_Publish_is_allowed
            {
                public And_PublishOnDestination_is_NOT_set()
                {
                    pasteMode.PublishOnDestination = null;
                }

                [Theory]
                [InlineData(true)]
                [InlineData(false)]
                void It_should_override_value_with_default(bool defaultValue)
                {
                    options.PasteDefaults.PublishOnDestination = defaultValue;

                    Assert.Equal(defaultValue,
                        PredefinedOptionsMerger.MergeWithAvailableOptions(pasteMode, options).PublishOnDestination);
                }
            }

            public class And_PublishOnDestination_is_set: When_controlling_Publish_is_allowed
            {
                public And_PublishOnDestination_is_set()
                {
                    pasteMode.PublishOnDestination = true;
                }

                [Theory]
                [InlineData(true)]
                [InlineData(false)]
                void It_should_NOT_override_value_with_default(bool defaultValue)
                {
                    options.PasteDefaults.PublishOnDestination = defaultValue;

                    Assert.True(PredefinedOptionsMerger.MergeWithAvailableOptions(pasteMode, options).PublishOnDestination);
                }
            }
        }

        public class When_controlling_CopyAllLanguageBranches_is_allowed : PredefinedOptionsMergerTests
        {
            public When_controlling_CopyAllLanguageBranches_is_allowed()
            {
                options.AllowedPasteActions.CopyAllLanguageBranches = true;
            }

            public class And_CopyAllLanguageBranches_is_NOT_set : When_controlling_CopyAllLanguageBranches_is_allowed
            {
                public And_CopyAllLanguageBranches_is_NOT_set()
                {
                    pasteMode.CopyAllLanguageBranches = null;
                }

                [Theory]
                [InlineData(true)]
                [InlineData(false)]
                void It_should_override_value_with_default(bool defaultValue)
                {
                    options.PasteDefaults.CopyAllLanguageBranches = defaultValue;

                    Assert.Equal(defaultValue,
                        PredefinedOptionsMerger.MergeWithAvailableOptions(pasteMode, options).CopyAllLanguageBranches);
                }
            }

            public class And_CopyAllLanguageBranches_is_set : When_controlling_CopyAllLanguageBranches_is_allowed
            {
                public And_CopyAllLanguageBranches_is_set()
                {
                    pasteMode.CopyAllLanguageBranches = true;
                }

                [Theory]
                [InlineData(true)]
                [InlineData(false)]
                void It_should_NOT_override_value_with_default(bool defaultValue)
                {
                    options.PasteDefaults.CopyAllLanguageBranches = defaultValue;

                    Assert.True(PredefinedOptionsMerger.MergeWithAvailableOptions(pasteMode, options).CopyAllLanguageBranches);
                }
            }
        }

        public class When_controlling_CopyDescendants_is_allowed : PredefinedOptionsMergerTests
        {
            public When_controlling_CopyDescendants_is_allowed()
            {
                options.AllowedPasteActions.CopyDescendants = true;
            }

            public class And_CopyDescendants_is_NOT_set : When_controlling_CopyDescendants_is_allowed
            {
                public And_CopyDescendants_is_NOT_set()
                {
                    pasteMode.CopyDescendants = null;
                }

                [Theory]
                [InlineData(true)]
                [InlineData(false)]
                void It_should_override_value_with_default(bool defaultValue)
                {
                    options.PasteDefaults.CopyDescendants = defaultValue;

                    Assert.Equal(defaultValue,
                        PredefinedOptionsMerger.MergeWithAvailableOptions(pasteMode, options).CopyDescendants);
                }
            }

            public class And_CopyDescendants_is_set : When_controlling_CopyDescendants_is_allowed
            {
                public And_CopyDescendants_is_set()
                {
                    pasteMode.CopyDescendants = true;
                }

                [Theory]
                [InlineData(true)]
                [InlineData(false)]
                void It_should_NOT_override_value_with_default(bool defaultValue)
                {
                    options.PasteDefaults.CopyDescendants = defaultValue;

                    Assert.True(PredefinedOptionsMerger.MergeWithAvailableOptions(pasteMode, options).CopyDescendants);
                }
            }
        }
    }
}
