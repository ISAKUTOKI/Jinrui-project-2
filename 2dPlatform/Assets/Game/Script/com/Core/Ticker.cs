using UnityEngine;

namespace com
{
    public class Ticker : MonoBehaviour
    {
        public float TickTime;
        float _nextTs;

        protected virtual void Update()
        {
            if (GameTime.time >= _nextTs)
            {
                _nextTs = GameTime.time + TickTime;
                Tick();
            }
        }

        protected virtual void Tick()
        {
        }
    }
}