using UnityEngine;

namespace com
{
    public class GameTime : MonoBehaviour
    {
        private static float _timeScale = 1;
        private static float _timeValidated = 0;
        private static float _cachedDt;
        private static float _timeCached = 0;

        public static float timeScale
        {
            get
            {
                return _timeScale;
            }
            set
            {
                ValidateTime();
                if (value < 0)
                {
                    _timeScale = 0;
                }
                else
                {
                    _timeScale = value;
                }
            }
        }

        public static float time
        {
            get
            {
                ValidateTime();
                return _timeValidated;
            }
        }

        public static float deltaTime
        {
            get
            {
                return Time.deltaTime * _timeScale;
            }
        }

        private static void ValidateTime()
        {
            var delta = Time.time - _timeCached;
            _timeCached = Time.time;
            _timeValidated += delta * _timeScale;
        }
    }
}