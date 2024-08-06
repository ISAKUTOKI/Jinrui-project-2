using UnityEngine;
using System.Collections.Generic;

namespace com
{
    [System.Serializable]
    public struct SoundInfo
    {
        public string name;
        public AudioClip ac;
    }

    [System.Serializable]
    public struct SoundInfoList
    {
        public string tag;
        public List<SoundInfo> siList;
    }

    public class SoundSystem : MonoBehaviour
    {
        public static SoundSystem instance { get; private set; }

        public List<SoundInfoList> soundList;
        public Dictionary<string, AudioClip> dic;

        private AudioSource _soundSource;

        private string _lastSound = "";
        private int _lastSoundFrame;

        void Awake()
        {
            instance = this;
            _soundSource = gameObject.AddComponent<AudioSource>();
            _lastSoundFrame = -1;

            AddToDic();
        }

        void AddToDic()
        {
            dic = new Dictionary<string, AudioClip>();
            foreach (var i in soundList)
            {
                foreach (var info in i.siList)
                {
                    dic.Add(info.name, info.ac);
                   //Debug.Log("dic " + info.name + " " + info.ac);
                }
            }
        }

        private bool IsEnabled()
        {
            return true;
        }

        public void Play(string soundName, float volume = 1)
        {
            if (string.IsNullOrEmpty(soundName))
                return;
            // Debug.Log("Play " + soundName);
            if (!IsEnabled())
                return;

            if (_lastSoundFrame == Time.frameCount && _lastSound == soundName)
                return;

            if (!dic.ContainsKey(soundName))
            {
                Debug.LogWarning("!ContainsKey " + soundName);
                return;
            }

            _soundSource.PlayOneShot(dic[soundName], volume);
            _lastSound = soundName;
            _lastSoundFrame = Time.frameCount;
        }

        public void Play(string[] soundNames, float volume = 1)
        {
            var soundName = soundNames[Random.Range(0, soundNames.Length)];
            Play(soundName, volume);
        }
    }
}