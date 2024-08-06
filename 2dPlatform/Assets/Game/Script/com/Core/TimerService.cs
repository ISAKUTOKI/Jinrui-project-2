using UnityEngine;

namespace com
{
    public class TimerService : MonoBehaviour
    {
        public bool useUnscaledTime;

        private float _timeSaved;
        private float _timeStampCache;
        private bool _paused;

    
        private float TimeStamp
        {
            get
            {
                return (useUnscaledTime ? Time.unscaledTime : Time.time);
            }
        }

        public void Restart()
        {
            _timeSaved = 0;
            _timeStampCache = TimeStamp;
            _paused = false;
        }

        public void Pause()
        {
            Validate();
            _paused = true;
        }
        public void Resume()
        {
            Validate();
            _paused = false;
        }

        public float GetTime()
        {
            Validate();
            return _timeSaved;
        }

        public void Validate()
        {
            if (!_paused)
            {
                _timeSaved += TimeStamp - _timeStampCache;
            }
          
            _timeStampCache = TimeStamp;
        }
    }
}