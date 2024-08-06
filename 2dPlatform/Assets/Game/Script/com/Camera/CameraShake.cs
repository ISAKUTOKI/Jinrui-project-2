using UnityEngine;
using System.Collections.Generic;

namespace com
{
    [System.Serializable]
    public class CameraShakeDetail
    {
        public CameraShake.ShakeLevel level;

        public int turn = 5;
        public float amplitude = 4;
        public float time = 1;
    }

    public class CameraShake : MonoBehaviour
    {
        public enum ShakeLevel
        {
            VeryStrong,
            Strong,
            Medium,
            VeryWeak,
            Weak,
            None,
        }

        public List<CameraShakeDetail> levels;
        private CameraShakeDetail _detail;
        public Transform self;
        private float _timer;

        public static CameraShake instance { get; private set; }

        void Start()
        {
            _timer = 0;
            instance = this;
        }

        void Update()
        {
            if (_timer > 0)
            {
                float t = _timer / _detail.time;
                float f = 1 - Mathf.Abs(2 * t - 1f);
                float amp = f * _detail.amplitude;
                self.localPosition = Vector3.up * amp * Mathf.Sin(Mathf.PI * t * 2 * _detail.turn);

                _timer -= Time.deltaTime;

                if (_timer <= 0)
                {
                    self.localPosition = Vector3.zero;
                }
            }
        }

        public void Shake(ShakeLevel level)
        {
            if (level == ShakeLevel.None)
                return;

            foreach (var lv in levels)
            {
                if (lv.level == level)
                {
                    _detail = lv;
                }
            }

            _timer = _detail.time;
        }
    }
}