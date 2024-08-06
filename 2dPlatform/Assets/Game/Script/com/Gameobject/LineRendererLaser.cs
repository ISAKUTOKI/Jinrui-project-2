using UnityEngine;

namespace com
{
    public class LineRendererLaser : MonoBehaviour
    {
        public AnimationCurve ac;
        public LineRenderer lr;
        public float multiplier = 1;
        public float duration = 1;
        private float _timer = 0;

        public void Play()
        {
            _timer = duration;
            lr.gameObject.SetActive(true);
        }

        public void Stop()
        {
            _timer = 0;
            lr.gameObject.SetActive(false);
        }

        void Update()
        {
            if (_timer <= 0)
            {
                return;
            }
            _timer -= com.GameTime.deltaTime;
            float f = 1 - _timer / duration;
            float v = ac.Evaluate(f);
            lr.widthMultiplier = v * multiplier;

            if (_timer <= 0)
            {
                Stop();
            }
        }
    }
}