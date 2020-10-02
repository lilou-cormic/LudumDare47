using UnityEngine;

namespace PurpleCable
{
    public class RotateAnimation : SimpleAnimation
    {
        [SerializeField] Vector3 EndLocalRotation = Vector3.zero;

        private Quaternion _deriv = Quaternion.identity;

        protected override void SetEndValue()
        {
            transform.localRotation = Quaternion.Euler(EndLocalRotation);
        }

        protected override bool MustUpdate()
        {
            return Vector3.Distance(transform.localRotation.eulerAngles, EndLocalRotation) > 0.01f;
        }

        protected override void UpdateValue(float t)
        {
            transform.localRotation = RotationExtensions.SmoothDamp(transform.localRotation, Quaternion.Euler(EndLocalRotation), ref _deriv, Duration);
        }
    }
}
