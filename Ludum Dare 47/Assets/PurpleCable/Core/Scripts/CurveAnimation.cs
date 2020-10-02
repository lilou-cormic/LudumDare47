using System.Linq;
using UnityEngine;

namespace PurpleCable
{
    public class CurveAnimation : SimpleAnimation
    {
        [SerializeField] Vector3[] LocalPositions = null;

        private int _index = 0;
        private Vector3 _nextLocalPosition = Vector3.zero;

        private Vector3 _positionVelocity = Vector3.zero;

        private void Awake()
        {
            if (LocalPositions == null || LocalPositions.Length == 0)
            {
                Destroy(gameObject);
                return;
            }

            _nextLocalPosition = LocalPositions[0];
        }

        protected override void SetEndValue()
        {
            transform.localPosition = LocalPositions.Last();
        }

        protected override bool MustUpdate()
        {
            if (_index >= LocalPositions.Length || _nextLocalPosition == null)
                return false;

            return Vector3.Distance(transform.localPosition, _nextLocalPosition) > 0.01f;
        }

        protected override void UpdateValue(float t)
        {
            transform.localPosition = Vector3.SmoothDamp(transform.localPosition, _nextLocalPosition, ref _positionVelocity, Duration / LocalPositions.Length);

            if (Vector3.Distance(transform.localPosition, _nextLocalPosition) <= 0.01f)
            {
                _index++;
                _nextLocalPosition = LocalPositions.ElementAtOrDefault(_index);
            }
        }

        private void OnDrawGizmos()
        {
            if (LocalPositions == null || LocalPositions.Length == 0)
                return;


            Vector3 GetPosition(int index)
            {
                return transform.position + (LocalPositions[index] - transform.localPosition);
            }

            Gizmos.DrawLine(transform.position, GetPosition(0));

            for (int i = 0; i < LocalPositions.Length - 1; i++)
            {
                Gizmos.DrawLine(GetPosition(i), GetPosition(i + 1));
            }
        }
    }
}
