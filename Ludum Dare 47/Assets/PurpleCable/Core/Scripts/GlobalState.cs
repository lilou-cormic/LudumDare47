using UnityEngine;

namespace PurpleCable
{
    public class GlobalState : MonoBehaviour
    {
        [SerializeField] string _StateName = null;
        public string StateName => _StateName;

        public bool Value { get; set; }
    }
}
