using System;
using UnityEngine;

namespace PurpleCable
{
    /// <summary>
    /// Score manager [MonoBehaviour]
    /// </summary>
    public class ScoreManager : MonoBehaviour
    {
        #region Properties

        /// <summary>
        /// Current score
        /// </summary>
        public static int Score { get; private set; } = 0;

        /// <summary>
        /// Best score
        /// </summary>
        public static int HighScore { get; private set; } = 0;

        /// <summary>
        /// Score multiplier
        /// </summary>
        public static int ScoreMultiplier { get; private set; } = 1;

        #endregion

        #region Events

        /// <summary>
        /// Triggers when the score changes (i.e. Score, HighScore or ScoreMultiplier changes)
        /// </summary>
        public static event Action ScoreChanged;

        #endregion

        #region Unity callbacks

        private void Awake()
        {
            // Get saved best score
            HighScore = PlayerPrefs.GetInt("HighScore", 0);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds points to current score and triggers ScoreChanged
        /// </summary>
        /// <param name="points">The number of points to add (can be negative)</param>
        /// <returns>The total points that were added (after applying the multiplier)</returns>
        public static int AddPoints(int points)
        {
            int pts = points * ScoreMultiplier;

            Score += pts;

            ScoreChanged?.Invoke();

            return pts;
        }

        /// <summary>
        /// Sets the best score, saves it, and triggers ScoreChanged
        /// </summary>
        public static void SetHighScore()
        {
            if (Score > HighScore)
            {
                HighScore = Score;

                PlayerPrefs.SetInt("HighScore", HighScore);
            }

            ScoreChanged?.Invoke();
        }

        /// <summary>
        /// Resets the current score to 0, multiplier to 1, and triggers ScoreChanged
        /// </summary>
        public static void ResetScore()
        {
            Score = 0;
            ScoreMultiplier = 1;

            ScoreChanged?.Invoke();
        }

        /// <summary>
        /// Resets the multiplier to 1 and triggers ScoreChanged
        /// </summary>
        public static void ResetScoreMultiplier()
        {
            ScoreMultiplier = 1;

            ScoreChanged?.Invoke();
        }

        /// <summary>
        /// Increments the multiplier [by 1]
        /// </summary>
        public static void IncrementMultiplier()
        {
            ScoreMultiplier++;

            ScoreChanged?.Invoke();
        }

        #endregion
    }
}
