using UnityEngine;

namespace com
{
    public class RotateBehaviour : MonoBehaviour
    {
        public float SpeedY;
        public float SpeedX;
        public float SpeedZ;
        public bool isLocal;
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
        public bool IsPlaying()
        {
            return !_stopped;
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

            var dt = com.GameTime.deltaTime;
            if (isLocal)
            {
                transform.localEulerAngles += new Vector3(SpeedX * dt, SpeedY * dt, SpeedZ * dt);
            }
            else
            {
                transform.eulerAngles += new Vector3(SpeedX * dt, SpeedY * dt, SpeedZ * dt);
            }
        }
    }
}