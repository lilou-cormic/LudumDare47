using TMPro;
using UnityEngine;

namespace PurpleCable
{
    public class Tooltip : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI TooltipText = null;

        public void Show(string text)
        {
            TooltipText.text = text;
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
