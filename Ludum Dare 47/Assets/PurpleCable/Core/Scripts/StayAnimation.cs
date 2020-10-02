using UnityEngine;

namespace PurpleCable
{
    public class StayAnimation : SimpleAnimation
    {
        Vector3 position = Vector3.zero;
        Quaternion rotation = Quaternion.identity;
        Vector3 scale = Vector3.one;

        private void Awake()
        {
            position = transform.position;
            rotation = transform.rotation;
            scale = transform.localScale;
        }

        protected override void SetEndValue()
        {
            transform.position = position;
            transform.rotation = rotation;
            transform.localScale = scale;
        }

        protected override bool MustUpdate()
        {
            return true;
        }

        protected override void UpdateValue(float t)
        {
            SetEndValue();
        }

        private void LateUpdate()
        {
            SetEndValue();
        }
    }
}
