using System.Web;
using EPiServer.ServiceLocation;

namespace ExtendedContentCopy
{
    /// <summary>
    /// Responsible for loading paste option selected by user in edit mode
    /// </summary>
    public interface IUserPasteModeLoader
    {
        PasteMode Load();
    }

    [ServiceConfiguration(typeof(IUserPasteModeLoader))]
    internal class UserPasteModeLoader : IUserPasteModeLoader
    {
        public PasteMode Load()
        {
            if (HttpContext.Current == null)
            {
                return null;
            }

            var headers = HttpContext.Current.Request.Headers;

            bool? GetValue(string key)
            {
                var value = headers[key];
                if (value == null)
                {
                    return null;
                }

                return value == "true";
            }

            if (GetValue("extendedPaste") != true)
            {
                return null;
            }

            return new PasteMode
            {
                PublishOnDestination = GetValue("extendedPastePublish"),
                CopyAllLanguageBranches = GetValue("extendedPasteLanguages"),
                CopyDescendants = GetValue("extendedPasteDescendants")
            };
        }
    }
}