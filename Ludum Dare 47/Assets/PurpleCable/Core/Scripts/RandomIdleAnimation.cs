using UnityEngine;

namespace PurpleCable
{
    public class RandomIdleAnimation : MonoBehaviour
    {
        #region Editor

        [SerializeField] Animator Animator = null;

        [Min(0f)]
        [SerializeField] float MinDelay = 0f;

        [Min(0f)]
        [SerializeField] float MaxDelay = 0f;

        [Min(1)]
        [SerializeField] int MinBurstCount = 1;

        [Min(1)]
        [SerializeField] int MaxBurstCount = 1;

        [Min(0)]
        [SerializeField] float BurstSpacing = 0.5f;

        #endregion

        #region Variables

        private float _timeLeft = 0f;

        private int _burstLeft = 1;

        #endregion

        #region Unity callbacks

        private void Awake()
        {
            if (Animator == null)
                Animator = GetComponent<Animator>();

            if (Animator == null)
                enabled = false;
        }

        private void Update()
        {
            if (_timeLeft > 0)
            {
                _timeLeft -= Time.deltaTime;

                return;
            }

            Animator.SetTrigger("Idle");
            _burstLeft--;

            if (_burstLeft > 0)
            {
                _timeLeft = BurstSpacing;
            }
            else
            {
                _burstLeft = Random.Range(MinBurstCount, MaxBurstCount + 1);
                _timeLeft = Random.Range(MinDelay, MaxDelay);
            }
        }

        #endregion
    }
}