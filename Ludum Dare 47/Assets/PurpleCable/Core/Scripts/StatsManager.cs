using System.Collections.Generic;
using UnityEngine;

namespace PurpleCable
{
    /// <summary>
    /// Stats manager (for game plaform APIs)
    /// </summary>
    public static class StatsManager
    {
        #region Data

        /// <summary>
        /// Stats API list
        /// </summary>
        private static List<IStatsAPI> _statsAPIs = new List<IStatsAPI>();

        #endregion

        #region Methods

        /// <summary>
        /// Adds a stats API to the list
        /// </summary>
        /// <param name="statsAPI"></param>
        public static void AddStatAPI(IStatsAPI statsAPI)
        {
            if (_statsAPIs.Contains(statsAPI))
                return;

            _statsAPIs.Add(statsAPI);
        }

        /// <summary>
        /// Submits a stat to all stat APIs after having saved it
        /// </summary>
        /// <param name="statName">The name of the stat</param>
        /// <param name="value">The value</param>
        public static void SubmitStat(string statName, int value)
        {
            PlayerPrefs.SetInt(statName, 1);

            foreach (var statsAPI in _statsAPIs)
            {
                statsAPI.SubmitStat(statName, value);
            }

            Debug.Log($"SubmitStat: {statName}, {value}");
        }

        /// <summary>
        /// Submits and achivement (stat set to 1)
        /// </summary>
        /// <param name="achivementName">The achivement [stat] name</param>
        public static void SubmitAchivement(string achivementName)
        {
            SubmitStat(achivementName, 1);
        }

        #endregion
    }

    #region IStatsAPI interface

    /// <summary>
    /// Interface for stats API
    /// </summary>
    public interface IStatsAPI
    {
        /// <summary>
        /// Submits a stat
        /// </summary>
        /// <param name="statName">The name of the stat</param>
        /// <param name="value">The value</param>
        void SubmitStat(string statName, int value);
    }

    #endregion
}
