using UnityEngine;
using System.Collections;
namespace com
{
    [System.Serializable]
    public class CommonBehaviourPropertyChangeData
    {
        public bool enabled;
        public bool stopped { get; private set; }
        public float startValue;
        public float endValue;

        public float delay;
        public float turnTime;

        public bool loop;
        public int loopTime;

        public bool pingpong;

        private bool _isPingpongTurn;
        private float _timer;
        private int _restTurns;//2 times if pingpong

        //can use animation curve, not implement now

        public void Stop()
        {
            stopped = true;
        }
        public void Start()
        {
            stopped = false;
        }

        public void Reset()
        {
            _timer = -delay;
            _isPingpongTurn = false;

            if (!loop)
            {
                _restTurns = 1;
            }
            else
            {
                _restTurns = loopTime;
                if (loopTime <= 0)
                {
                    _restTurns = -1;//infinitive
                }
            }

            if (pingpong && _restTurns > 0)
            {
                _restTurns *= 2;
            }
        }

        public float Tick(float dt)
        {
            _timer += dt;
            float f = Mathf.Clamp01(_timer / turnTime);
            float res = 0;
            if (pingpong && _isPingpongTurn)
            {
                res = Mathf.Lerp(startValue, endValue, 1 - f);
            }
            else
            {
                res = Mathf.Lerp(startValue, endValue, f);
            }

            if (f >= 1)
            {
                _timer = 0;
                if (_restTurns < 0)
                {
                    //do nothing
                }
                else if (_restTurns <= 1)
                {
                    _restTurns = 0;
                    stopped = true;
                }
                else
                {
                    _restTurns -= 1;
                    if (pingpong)
                    {
                        _isPingpongTurn = !_isPingpongTurn;
                    }
                }
            }

            return res;
        }
    }
}