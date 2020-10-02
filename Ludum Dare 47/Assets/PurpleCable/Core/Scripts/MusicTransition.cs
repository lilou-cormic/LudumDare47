using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PurpleCable
{
    public class MusicTransition : Singleton<MusicTransition>
    {
        private static Dictionary<string, AudioSource> Musics = null;

        private static AudioSource _currentMusic = null;
        private static float _volume = 0.2f;

        private void Awake()
        {
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

        public static void Play(string musicName)
        {
            AudioSource currentMusic = _currentMusic;
            AudioSource nextMusic = null;

            if (!string.IsNullOrWhiteSpace(musicName) && Musics.ContainsKey(musicName))
                nextMusic = Musics[musicName];

            if (currentMusic == nextMusic)
                return;

            _currentMusic = nextMusic;

            if (currentMusic != null || nextMusic != null)
            {
                if (Instance != null)
                {
                    Instance.StopAllCoroutines();

                    Instance.StartCoroutine(TransitionMusic(currentMusic, nextMusic));
                }
                else
                {
                    if (currentMusic != null)
                        currentMusic.volume = 0;

                    if (nextMusic != null)
                        nextMusic.volume = _volume;
                }
            }
        }

        private static IEnumerator TransitionMusic(AudioSource currentMusic, AudioSource nextMusic)
        {
            bool ok = true;

            do
            {
                ok = true;

                if (currentMusic != null && currentMusic.volume > 0)
                {
                    currentMusic.volume -= 0.01f;
                    ok = false;

                    if (currentMusic.volume < 0)
                        currentMusic.volume = 0;
                }

                if (nextMusic != null && nextMusic.volume < _volume)
                {
                    nextMusic.volume += 0.01f;
                    ok = false;

                    if (nextMusic.volume > _volume)
                        nextMusic.volume = _volume;
                }

                yield return new WaitForSeconds(0.1f);

            } while (!ok);

            if (currentMusic != null)
                currentMusic.volume = 0;

            if (nextMusic != null)
                nextMusic.volume = _volume;
        }
    }
}
