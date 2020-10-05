using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PurpleCable
{
    public class MusicTransition : Singleton<MusicTransition>
    {
        [SerializeField] float _TransitionSpeed = 0.01f;
        private static float TransitionSpeed = 0.01f;

        private static Dictionary<string, AudioSource> Musics = null;

        private static AudioSource _currentMusic = null;
        private static float _volume = 0.2f;

        private void Awake()
        {
            TransitionSpeed = _TransitionSpeed;

            SetVolume();

            if (Musics == null)
            {
                Musics = Resources.LoadAll<AudioClip>("Musics").ToDictionary(x => x.name, y => CreateAudioSource(y));

                foreach (var music in Musics.Values)
                {
                    music.Play();
                }
            }
        }

        private AudioSource CreateAudioSource(AudioClip audioClip)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = true;
            audioSource.playOnAwake = false;
            audioSource.volume = 0;
            audioSource.clip = audioClip;

            return audioSource;
        }

        public static void SetVolume()
        {
            _volume = (MusicPlayer.Instance?.gameObject.activeSelf != false ? MusicPlayer.Volume : 0);

            if (_currentMusic != null)
                _currentMusic.volume = _volume;
        }

        public static void Play(AudioSource nextMusic, float volumMultiplier = 1)
        {
            AudioSource currentMusic = _currentMusic;

            if (currentMusic == nextMusic)
                return;

            _currentMusic = nextMusic;

            if (currentMusic != null || nextMusic != null)
            {
                if (Instance != null)
                {
                    Instance.StopAllCoroutines();

                    Instance.StartCoroutine(TransitionMusic(currentMusic, nextMusic, volumMultiplier));
                }
                else
                {
                    if (currentMusic != null)
                        currentMusic.volume = 0;

                    if (nextMusic != null)
                        nextMusic.volume = _volume * volumMultiplier;
                }
            }
        }

        public static void Play(string musicName)
        {
            AudioSource nextMusic = null;

            if (!string.IsNullOrWhiteSpace(musicName) && Musics.ContainsKey(musicName))
                nextMusic = Musics[musicName];

            Play(nextMusic);
        }

        private static IEnumerator TransitionMusic(AudioSource currentMusic, AudioSource nextMusic, float volumMultiplier = 1)
        {
            bool ok = true;

            float volume = _volume * volumMultiplier;

            do
            {
                ok = true;

                if (currentMusic != null && currentMusic.volume > 0)
                {
                    currentMusic.volume -= TransitionSpeed;
                    ok = false;

                    if (currentMusic.volume < 0)
                        currentMusic.volume = 0;
                }

                if (nextMusic != null && nextMusic.volume < volume)
                {
                    nextMusic.volume += TransitionSpeed;
                    ok = false;

                    if (nextMusic.volume > volume)
                        nextMusic.volume = volume;
                }

                yield return new WaitForSeconds(0.1f);

            } while (!ok);

            if (currentMusic != null)
                currentMusic.volume = 0;

            if (nextMusic != null)
                nextMusic.volume = volume;
        }
    }
}
