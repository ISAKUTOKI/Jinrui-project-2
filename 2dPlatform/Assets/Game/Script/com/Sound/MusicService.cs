using UnityEngine;
using System;
using DG.Tweening;
using UnityEngine.Audio;

namespace com
{
    public class MusicService : MonoBehaviour
    {
        public enum Musics
        {
            MenuMusicVolume,
            CombatMusicVolume,
            BossMusicVolume,
            None,
            MusicVolume,
        }
        public static MusicService instance { get; private set; }

        void Awake()
        {
            instance = this;

            InitEnumToSource();
            // IsEnabled = _storage.GetBool(saveKey, true);
            IsEnabled = false;
            //  _adService.SignToAdEvents(OnAdStarted, OnAdEnded);
        }

        public AudioMixer masterMixer;
        public AudioSource menuAudioSource;
        public AudioSource combatAudioSource;
        public AudioSource bossAudioSource;
        private Musics[] _musics = new Musics[] {
            Musics.MenuMusicVolume,
            Musics.CombatMusicVolume,
            Musics.BossMusicVolume
        };
        public float transitionTime = 1.5f;
        private Musics LastMusic = Musics.MenuMusicVolume;
        private AudioSource[] _enumToSource;

        void PlayMusic(Musics m)
        {
            //Debug.LogWarning("PlayMusic!!!! " + m);
            SetLastMusic(m);
            foreach (var music in _musics)
            {
                if (music == m)
                {
                    TurnOnMusic(music);
                }
                else
                {
                    TurnOffMusic(music);
                }
            }
        }

        public void PlayCombat()
        {
            PlayMusic(Musics.CombatMusicVolume);
            //PlayMusic(Musics.None);
            //MusicService.instance.FadeOutMusic(MusicService.Musics.MenuMusicVolume);
            //MusicService.instance.FadeOutMusic(MusicService.Musics.BossMusicVolume);
            //MusicService.instance.FadeInMusic(MusicService.Musics.CombatMusicVolume, 2f);
        }

        public void PlayBoss()
        {
            PlayMusic(Musics.BossMusicVolume);
        }

        public void PlayMenu()
        {
            PlayMusic(Musics.MenuMusicVolume);
        }

        private void OnAdEnded(bool obj)
        {
            FadeInMusic();
        }

        private void OnAdStarted()
        {
            FadeOutMusic();
        }

        private void InitEnumToSource()
        {
            _enumToSource = new AudioSource[3];
            _enumToSource[(int)Musics.CombatMusicVolume] = combatAudioSource;
            _enumToSource[(int)Musics.MenuMusicVolume] = menuAudioSource;
            _enumToSource[(int)Musics.BossMusicVolume] = bossAudioSource;

            TurnOffMusic(Musics.CombatMusicVolume);
            TurnOffMusic(Musics.MenuMusicVolume);
            TurnOffMusic(Musics.BossMusicVolume);
        }

        private void SetMusic(string target, int value, float delay = 0f)
        {
            DOTween.To(() => { float ans; masterMixer.GetFloat(target, out ans); return ans; }, x => masterMixer.SetFloat(target, x), value, transitionTime).SetDelay(delay);
        }

        public void FadeOutMusic()
        {
            SetMusic(Musics.MusicVolume.ToString(), EnabledToDecibels(false));
        }

        public void FadeInMusic()
        {
            //if (_soundService.MusicIsEnabled)
            SetMusic(Musics.MusicVolume.ToString(), EnabledToDecibels(true));
        }

        public void TurnOffMusic(Musics target)
        {
            masterMixer.SetFloat(target.ToString(), EnabledToDecibels(false));
            _enumToSource?[(int)target]?.Stop();
        }

        public void TurnOnMusic(Musics target)
        {
            SetLastMusic(target);
            masterMixer.SetFloat(target.ToString(), EnabledToDecibels(true));
            if (IsEnabled)
            {
                _enumToSource[(int)target]?.Play();
            }
        }

        public void FadeOutMusic(Musics target)
        {
            string targetProperty = target.ToString();
            Debug.Log(targetProperty);
            float endValue = EnabledToDecibels(false);
            DOTween.To(() => { float ans; masterMixer.GetFloat(targetProperty, out ans); return ans; },
                x => masterMixer.SetFloat(targetProperty, x),
                endValue, transitionTime)
                .SetEase(Ease.InExpo)
                .OnComplete(_enumToSource[(int)target].Stop);
        }

        public void FadeInMusic(Musics target, float delay = 0f)
        {
            SetLastMusic(target);
            int t = (int)target;
            if (_enumToSource == null || _enumToSource[t] == null)
                return;
            var source = _enumToSource[t];
            string targetProperty = target.ToString();
            float endValue = EnabledToDecibels(true);
            if (source == null)
            {
                return;
            }
            if (IsEnabled && !source.isPlaying)
            {
                DOTween.To(() => { float ans; masterMixer.GetFloat(targetProperty, out ans); return ans; },
                    x => masterMixer.SetFloat(targetProperty, x),
                    endValue, transitionTime)
                    .SetEase(Ease.OutExpo)
                    .SetDelay(delay);
                source.Play();
            }
        }

        private void SetLastMusic(Musics target)
        {
            LastMusic = target;
        }

        private int EnabledToDecibels(bool isEnabled)
        {
            return isEnabled ? -1 : -80;
        }

        private bool _isEnabled;
        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                _isEnabled = combatAudioSource.enabled = bossAudioSource.enabled = menuAudioSource.enabled = value;
                //Debug.Log("_isEnabled"+ _isEnabled);
                if (value)
                {
                    PlayMusic();
                    masterMixer.SetFloat(Musics.MenuMusicVolume.ToString(), EnabledToDecibels(LastMusic == Musics.MenuMusicVolume));
                    masterMixer.SetFloat(Musics.CombatMusicVolume.ToString(), EnabledToDecibels(LastMusic == Musics.CombatMusicVolume));
                    masterMixer.SetFloat(Musics.BossMusicVolume.ToString(), EnabledToDecibels(LastMusic == Musics.BossMusicVolume));
                }
                // _storage.SetBool(saveKey, value);
            }
        }

        private void PlayMusic()
        {
            Debug.Log("PlayMusic");
            int i = (int)LastMusic;
            var source = _enumToSource[i];
            if (source != null)
            {
                source.Play();
            }
        }

        public void SetTemporaryEnabled(bool value)
        {
            if (value)
            {
                FadeInMusic();
            }
            else
            {
                FadeOutMusic();
            }
        }
    }
}