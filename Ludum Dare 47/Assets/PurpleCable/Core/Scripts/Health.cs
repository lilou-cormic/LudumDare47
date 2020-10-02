using System;
using UnityEngine;

namespace PurpleCable
{
    /// <summary>
    /// Health [MonoBehaviour]
    /// </summary>
    public class Health : MonoBehaviour
    {
        #region Editor

        /// <summary>
        /// Max HP
        /// </summary>
        [SerializeField] int _MaxHP;

        #endregion

        #region Properties

        /// <summary>
        /// Max HP
        /// </summary>
        public int MaxHP { get => _MaxHP; set { _MaxHP = value; CurrentHP = _MaxHP; } }

        /// <summary>
        /// Current HP [0 - MaxHP]
        /// </summary>
        public int CurrentHP { get; private set; }

        /// <summary>
        /// Percentaga of curent HP on Max HP [0 - 1]
        /// </summary>
        public float Percent => CurrentHP / (float)MaxHP;

        #endregion

        #region Events

        /// <summary>
        /// Triggered when a Health is "added" (enabled)
        /// </summary>
        public static event Action<Health> HealthAdded;

        /// <summary>
        /// Triggered when a Health is "removed" (disabled)
        /// </summary>
        public static event Action<Health> HealthRemoved;

        /// <summary>
        /// Triggered when CurrentHP value changed
        /// </summary>
        public event Action<Health> HPChanged;

        /// <summary>
        /// Triggered when CurrentHP value is 0 (depleted)
        /// </summary>
        public event Action<Health> HPDepleted;

        #endregion

        #region Unity callbacks

        /// <summary>
        /// Initializes the HP and triggers HealthAdded
        /// </summary>
        private void OnEnable()
        {
            CurrentHP = MaxHP;
            HealthAdded?.Invoke(this);
        }

        /// <summary>
        /// Triggers HealthRemoved
        /// </summary>
        private void OnDisable()
        {
            HealthRemoved?.Invoke(this);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Changes the HP by a certain amount and triggers HPChanged (might trigger HPDelepleted)
        /// </summary>
        /// <param name="amount">The amount (can be negative)</param>
        public void ChangeHP(int amount)
        {
            // Change current HP by the specified ammout, remaning between 0 and MaxHP
            CurrentHP = Mathf.Clamp(CurrentHP + amount, 0, MaxHP);

            // Trigger HPChanged
            HPChanged?.Invoke(this);

            // If current HP is 0, trigger HPDepleted
            if (CurrentHP == 0)
                HPDepleted?.Invoke(this);
        }

        #endregion
    }
}
