using UnityEngine;
using UnityEngine.EventSystems;

namespace PurpleCable
{
    public class SocialMedia : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private SocialMediaWebsite Website = SocialMediaWebsite.Unknown;

        [SerializeField]
        private string UserOrUrl = null;

        private string FullUrl => GetURL(Website, UserOrUrl);

        public void Click()
        {
#if UNITY_WEBGL
            Application.ExternalEval("window.open(\"" + FullUrl + "\")");
#else
            Application.OpenURL(FullUrl);
#endif
        }

        private static string GetURL(SocialMediaWebsite website, string userOrUrl)
        {
            switch (website)
            {

                case SocialMediaWebsite.Twitter:
                    return $@"https://twitter.com/{userOrUrl}";

                case SocialMediaWebsite.Itch:
                    return $@"https://{userOrUrl}.itch.io/";

                case SocialMediaWebsite.Unknown:
                default:
                    return userOrUrl;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            TooltipManager.ShowTooltip(FullUrl);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            TooltipManager.HideTooltip();
        }

        public enum SocialMediaWebsite
        {
            Unknown = 0,

            Twitter = 1,

            Itch = 2,
        }
    }
}
