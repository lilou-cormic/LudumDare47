using TMPro;
using UnityEngine;

namespace PurpleCable
{
    public class UIScoreMultiplier : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI ScoreMultiplierText = null;

        [SerializeField]
        private string Format = "00";

        private void Start()
        {
            SetScoreMultiplierText();

            ScoreManager.ScoreChanged += ScoreManager_ScoreChanged;
        }

        private void OnDestroy()
        {
            ScoreManager.ScoreChanged -= ScoreManager_ScoreChanged;
        }

        private void ScoreManager_ScoreChanged()
        {
            SetScoreMultiplierText();
        }

        private void SetScoreMultiplierText()
        {
            if (ScoreMultiplierText != null)
                ScoreMultiplierText.text = "× " + ScoreManager.ScoreMultiplier.ToString(Format);
        }
    }
}
