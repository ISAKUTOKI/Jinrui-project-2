using UnityEngine;

namespace com
{
    public class BlinkCanvasGroup : MonoBehaviour
    {
        public float TurnTime;
        private float _timer;
        public bool Loop;
        public float StartAlpha;
        public float EndAlpha;
        public bool Pingpong;
        public bool StartAwake;
        public CanvasGroup cg;
        private bool _paused;
        public BlinkTipBehaviour btb;

        void Awake()
        {
            if (cg == null)
                cg = GetComponent<CanvasGroup>();

            _paused = true;
        }

        private void OnEnable()
        {
            if (StartAwake)
                Play();
        }

        public void Play()
        {
            cg.alpha = StartAlpha;
            _timer = TurnTime;
            _paused = false;
        }

        public void Stop()
        {
            cg.alpha = 0;
            _paused = true;
        }

        void Update()
        {
            if (_paused)
                return;

            _timer -= com.GameTime.deltaTime;
            if (_timer <= 0)
            {
                if (Loop)
                {
                    btb.OnLoopStart();
                    Play();
                }
                else
                {
                    cg.alpha = Pingpong ? StartAlpha : EndAlpha;
                    _paused = true;
                }

                return;
            }

            var f = _timer / TurnTime;
            var t = 1 - f;
            if (Pingpong)
                t = 1 - Mathf.Abs(1 - 2 * f);

            cg.alpha = Mathf.Lerp(StartAlpha, EndAlpha, t);
        }
    }

}