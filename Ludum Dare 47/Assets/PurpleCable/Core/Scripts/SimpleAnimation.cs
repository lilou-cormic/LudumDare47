using System.Collections;
using UnityEngine;

namespace PurpleCable
{
    public abstract class SimpleAnimation : MonoBehaviour
    {
        [SerializeField] public float Duration = 0.3f;

        [SerializeField] protected float Delay = 0f;

        public float TotalDuration => Delay + Duration;

        public bool IsAnimating { get; private set; } = false;

        public bool IsDoneAnimating { get; private set; } = false;

        private float _timer = 0f;

        private bool _isUpdateDone = false;

        protected void Update()
        {
            if (!IsAnimating || IsDoneAnimating)
                return;

            if (_isUpdateDone || _timer >= TotalDuration * 5)
            {
                SetEndValue();

                EndAnimation();

                return;
            }

            _timer += Time.deltaTime;

            if (_timer >= Delay)
            {
                if (!_isUpdateDone)
                {
                    if (MustUpdate())
                        UpdateValue(_timer / Duration);
                    else
                        _isUpdateDone = true;
                }
            }
        }

        public void StartAnimation()
        {
            _timer = 0f;
            _isUpdateDone = false;
            IsAnimating = true;
            IsDoneAnimating = false;
            gameObject.SetActive(true);
            this.enabled = true;
        }

        public void EndAnimation()
        {
            IsAnimating = false;
            IsDoneAnimating = true;

            this.enabled = false;
        }

        protected abstract void SetEndValue();

        protected abstract bool MustUpdate();

        protected abstract void UpdateValue(float t);
    }
}
