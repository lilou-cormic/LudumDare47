using UnityEngine;

namespace PurpleCable
{
    /// <summary>
    /// Tooltip manager [MonoBehaviour]
    /// </summary>
    public class TooltipManager : MonoBehaviour
    {
        #region Editor

        [SerializeField] Tooltip TooltipPrefab = null;

        #endregion

        #region Instance

        /// <summary>
        /// Current instance
        /// </summary>
        private static TooltipManager _instance = null;

        #endregion

        #region Properties

        private Tooltip _tooltip = null;

        public static bool IsTooltipAutomatic { get; set; } = true;

        public bool IsScreenSpace = false;

        private Camera Camera;

        #endregion

        #region Unity callbacks

        private void Start()
        {
            _instance = this;

            Camera = Camera.main;

            _tooltip = Instantiate(TooltipPrefab, transform);
            _tooltip.Hide();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Shows a tooltip
        /// </summary>
        /// <param name="tooltip">The tooltip text</param>
        private void ShowTooltipInternal(string tooltip)
        {
            if (_tooltip == null)
                return;

            if (string.IsNullOrWhiteSpace(tooltip))
                return;

            if (IsScreenSpace)
                _tooltip.transform.position = Input.mousePosition;
            else
                _tooltip.transform.position = (Vector2)Camera.ScreenToWorldPoint(Input.mousePosition);

            if (!_tooltip.gameObject.activeSelf)
                _tooltip.Show(tooltip);
        }

        /// <summary>
        /// Shows a tooltip
        /// </summary>
        /// <param name="tooltip">The tooltip text</param>
        public static void ShowTooltip(string tooltip)
        {
            _instance?.ShowTooltipInternal(tooltip);
        }

        /// <summary>
        /// Shows a tooltip based on a display name and a keyword
        /// </summary>
        /// <param name="displayName">The display name</param>
        /// <param name="keyword">The keyword</param>
        /// <remarks>[*keyword* -] *displayName*</remarks>
        private void ShowTooltipInternal(string displayName, string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                ShowTooltip(displayName);
            else
                ShowTooltip(keyword + " - " + displayName);
        }

        /// <summary>
        /// Shows a tooltip based on a display name and a keyword
        /// </summary>
        /// <param name="displayName">The display name</param>
        /// <param name="keyword">The keyword</param>
        /// <remarks>[*keyword* -] *displayName*</remarks>
        public static void ShowTooltip(string displayName, string keyword)
        {
            _instance?.ShowTooltipInternal(displayName, keyword);
        }

        /// <summary>
        /// Hides the tooltip
        /// </summary>
        public static void HideTooltip()
        {
            if (_instance != null && _instance._tooltip != null)
                _instance._tooltip.Hide();
        }

        #endregion
    }
}
