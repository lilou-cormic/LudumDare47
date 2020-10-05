using UnityEngine;

namespace PurpleCable
{
    [DisallowMultipleComponent]
    public class Music : MonoBehaviour
    {
        [SerializeField] string _MusicName = null;
        string MusicName => _MusicName;

        [SerializeField] AudioSource _AudioSource = null;
        AudioSource AudioSource => _AudioSource;

        [SerializeField] float VolumeMultiplier = 1;

        public bool PlayOnStart = true;

        private void Start()
        {
            if (PlayOnStart)
                Play();
        }

        public void Play()
        {
            if (!string.IsNullOrWhiteSpace(MusicName))
                MusicTransition.Play(MusicName);

            if (AudioSource != null && AudioSource.clip != null)
                MusicTransition.Play(AudioSource, VolumeMultiplier);
        }
    }
}
