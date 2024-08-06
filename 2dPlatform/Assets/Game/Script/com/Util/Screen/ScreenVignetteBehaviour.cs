using UnityEngine;
namespace com
{
    public class ScreenVignetteBehaviour : MonoBehaviour
    {
        //red screen wound effect
        public CanvasGroup Cg;
        public float Speed = 2.5f;

        private float _targetValue;
        private bool _isBlinking = false;
        private bool _isBlinkingForward = false;

        void Start()
        {
            ChangeValue(0);
        }

        private void Update()
        {
            if (_isBlinking)
            {
                var dt = Time.deltaTime;
                if (_isBlinkingForward)
                {
                    if (Cg.alpha >= _targetValue)
                    {
                        _isBlinkingForward = false;
                        _targetValue = 0;
                    }
                    else
                    {
                        var vMax = Cg.alpha + dt * Speed;
                        Cg.alpha = Mathf.Min(1, vMax);
                    }
                }
                else
                {
                    if (Cg.alpha <= _targetValue)
                    {
                        _isBlinking = false;
                    }
                    else
                    {
                        Cg.alpha = Mathf.Max(Cg.alpha - dt * Speed, 0);
                    }
                }
            }
        }

        public void ChangeValue(float v)
        {
            Cg.alpha = v;
            _isBlinking = false;
        }

        public void BlinkToValue(float v)
        {
            _isBlinking = true;
            _targetValue = Mathf.Max(Mathf.Min(v, 1), 0);
            _isBlinkingForward = true;
        }
    }
}