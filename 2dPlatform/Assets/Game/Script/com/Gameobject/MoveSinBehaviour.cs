using UnityEngine;
//using DG.Tweening;

namespace com
{
    public class MoveSinBehaviour : MonoBehaviour
    {
        public float freq;
        public float offset;
        public float amplitude;
        public bool xAxis;
        public bool yAxis;
        public bool zAxis;
        public bool localPos;
        public bool startPlay = true;
        private bool _stopped;
        private Vector3 _startPos;

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
            if (localPos)
            {
                _startPos = transform.localPosition;
            }
            else
            {
                _startPos = transform.position;
            }

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

            var newPos = _startPos;
            if (xAxis)
            {
                newPos += Vector3.right * Mathf.Sin(com.GameTime.time * freq + offset) * amplitude;
            }
            if (yAxis)
            {
                newPos += Vector3.up * Mathf.Sin(com.GameTime.time * freq + offset) * amplitude;
            }
            if (zAxis)
            {
                newPos += Vector3.forward * Mathf.Sin(com.GameTime.time * freq + offset) * amplitude;
            }

            if (localPos)
            {
                transform.localPosition = newPos;
            }
            else
            {
                transform.position = newPos;
            }
        }
    }
}