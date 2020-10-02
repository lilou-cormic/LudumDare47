using UnityEngine;

namespace PurpleCable
{
    public class MoveAnimation : SimpleAnimation
    {
        [SerializeField] Vector3 EndLocalPosition = Vector3.zero;

        private Vector3 _positionVelocity = Vector3.zero;

        protected override void SetEndValue()
        {
            transform.localPosition = EndLocalPosition;
        }

        protected override bool MustUpdate()
        {
            return Vector3.Distance(transform.localPosition, EndLocalPosition) > 0.01f;
        }

        protected override void UpdateValue(float t)
        {
            transform.localPosition = Vector3.SmoothDamp(transform.localPosition, EndLocalPosition, ref _positionVelocity, Duration);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(transform.position, transform.position + (EndLocalPosition - transform.localPosition));
        }
    }
}
