using TMPro;
using UnityEngine;

namespace PurpleCable
{
    public class UIScore : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI ScoreText = null;

        [SerializeField]
        private string ScoreLabel = "Score: ";

        [SerializeField]
        private TextMeshProUGUI BestText = null;

        [SerializeField]
        private string BestLabel = "Best: ";

        [SerializeField]
        private string Format = "00000";

        private void Start()
        {
            SetScoreTexts();

            if (ScoreLabel == null)
                ScoreLabel = string.Empty;

            if (BestLabel == null)
                BestLabel = string.Empty;

            ScoreManager.ScoreChanged += ScoreManager_ScoreChanged;
        }

        private void OnDestroy()
        {
            ScoreManager.ScoreChanged -= ScoreManager_ScoreChanged;
        }

        private void SetScoreTexts()
        {
            if (ScoreText != null)
                ScoreText.text = $"{ScoreLabel}{ScoreManager.Score.ToString(Format)}";

            if (BestText != null)
                BestText.text = $"{BestLabel}{ScoreManager.HighScore.ToString(Format)}";
        }

        private void ScoreManager_ScoreChanged()
        {
            SetScoreTexts();
        }
    }
}
