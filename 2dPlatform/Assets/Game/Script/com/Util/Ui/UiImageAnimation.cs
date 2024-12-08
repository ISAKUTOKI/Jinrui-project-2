using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com
{
    public class UiImageAnimation : MonoBehaviour
    {
        [SerializeField] float _interval;
        [SerializeField] UiImageAnimationClip[] _clips;
        [SerializeField] bool _startPlay;

        [SerializeField] Image _img;

        UiImageAnimationClip _clip;
        int _index;
        bool _playing;
        float _nextTimestamp;

        Action _playEndCallback;

        List<UiImageAnimationClip> _queue = new List<UiImageAnimationClip>();

        [System.Serializable]
        public class UiImageAnimationClip
        {
            public Sprite[] sps;
            public bool loop;
            public float speedRatio = 1;
        }

        private void Awake()
        {
            if (_img == null)
                _img = GetComponent<Image>();
        }

        void Start()
        {
            if (_startPlay)
                Play(0);
        }

        public UiImageAnimationClip GetClip(int i)
        {
            return _clips[i];
        }

        public bool IsPlayingClip(UiImageAnimationClip c)
        {
            return _clip == c;
        }

        public void Play(int i)
        {
            Play(GetClip(i));
        }

        public void ToggleDisplay(bool b)
        {
            _img.enabled = b;
        }

        public void Stop()
        {
            _playing = false;
            _clip = null;
            _img.enabled = false;
        }

        public void Play(UiImageAnimationClip c)
        {
            _img.enabled = true;
            _clip = c;
            _index = 0;
            _playing = true;
            _nextTimestamp = Time.unscaledTime;
            _img.sprite = _clip.sps[0];
        }

        public void SetPlayEndCallback(Action a)
        {
            _playEndCallback = a;
        }

        public void AddPlayQueue(UiImageAnimationClip c)
        {
            _queue.Add(c);
        }
        public void AddPlayQueueToNext(UiImageAnimationClip c)
        {
            AddPlayQueueIfIndex(c, 0);
        }
        public void AddPlayQueueIfIndex(UiImageAnimationClip c, int i)
        {
            _queue.Insert(i, c);
        }

        public void AbortPlayQueue()
        {
            _queue.Clear();
        }

        private void Update()
        {
            if (!_playing)
                return;

            if (Time.unscaledTime > _nextTimestamp)
            {
                _img.sprite = _clip.sps[_index++];
                _nextTimestamp = Time.unscaledTime + _interval / _clip.speedRatio;
                if (_index >= _clip.sps.Length)
                {
                    if (_clip.loop)
                    {
                        _index = 0;
                    }
                    else
                    {
                        if (_queue.Count > 0)
                        {
                            Play(_queue[0]);
                            _queue.RemoveAt(0);
                        }
                        else
                        {
                            Stop();
                            if (_playEndCallback != null)
                                _playEndCallback?.Invoke();
                        }
                    }

                }
            }
        }
    }
}