using UnityEngine;

namespace PurpleCable
{
    public class UISoundVolumeSlider : UISlider
    {
        [SerializeField]
        private AudioClip TestSound = null;

        private float _coolDown = 0.5f;
        private float _timeLeft = 0f;

        private void Update()
        {
            if (_timeLeft > 0)
                _timeLeft -= Time.deltaTime;
        }

        protected override float GetStartValue()
        {
            return SoundPlayer.Volume;
        }

        protected override void OnValueChanged()
        {
            SoundPlayer.Volume = Value;

            PlayerPrefs.SetFloat("SoundVolume", SoundPlayer.Volume);

            if (_timeLeft <= 0)
            {
                SoundPlayer.Play(TestSound);
                _timeLeft = _coolDown;
            }
        }
    }
}
