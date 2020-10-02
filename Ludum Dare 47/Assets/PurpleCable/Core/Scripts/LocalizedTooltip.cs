using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PurpleCable
{
    public class LocalizedTooltip : MonoBehaviour
    {
        [SerializeField] string TooltipValue = null;

        private void OnMouseEnter()
        {
            if (!string.IsNullOrWhiteSpace(TooltipValue))
                TooltipManager.ShowTooltip(TextManager.GetText(TooltipValue));
        }

        private void OnMouseExit()
        {
            TooltipManager.HideTooltip();
        }

        public void OnPointerEnter(PointerEventData eventData)
            => OnMouseEnter();

        public void OnPointerExit()
            => OnMouseExit();
    }
}
