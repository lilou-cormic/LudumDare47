using UnityEngine;

namespace PurpleCable
{
    [DisallowMultipleComponent]
    public class Music : MonoBehaviour
    {
        [SerializeField] string _MusicName = null;
        string MusicName => _MusicName;

        private void Start()
        {
            if (!string.IsNullOrWhiteSpace(MusicName))
                MusicTransition.Play(MusicName);
        }
    }
}
