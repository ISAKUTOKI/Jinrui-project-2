using UnityEngine;
//using DG.Tweening;

namespace com
{
    public class ScaleBehaviour : MonoBehaviour
    {
        public float maxValue;
        public float minValue;
        public float speed;
        public bool startPlay = true;
        private bool _stopped;

        public void Stop()
        {
            _stopped = true;
        }
        public void Play()
        {
            _stopped = false;
        }
        private void Start()
        {
            if (startPlay)
            {
                Play();
            }
            else
            {
                Stop();
            }
        }
        void Update()
        {
            if (_stopped)
            {
                return;
            }

            float s = minValue +(maxValue- minValue) * (Mathf.Sin(com.GameTime.time*speed)+1)*0.5f;
            transform.localScale = Vector3.one * s;
        }
    }
}