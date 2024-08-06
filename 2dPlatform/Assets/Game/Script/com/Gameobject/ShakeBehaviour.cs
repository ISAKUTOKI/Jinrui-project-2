using UnityEngine;
using DG.Tweening;

namespace com
{
    public class ShakeBehaviour : MonoBehaviour
    {
        public float amplitude = 1;
        public float duration = 1;
        public Transform target;
        private Vector3 _pos;
        public bool StartShake = false;

        public void Awake()
        {
            if (target == null)
                target = transform;

            _pos = target.transform.localPosition;
            if (StartShake)
            {
                ShakeLoop();
            }
        }

        private void OnEnable()
        {
            ResetPos();
        }

        private void ShakeLoop()
        {
            target.DOShakePosition(duration, amplitude, 10, 80, false, false).OnComplete(() =>
                {
                    ResetPos();
                    ShakeLoop();
                });
        }

        public void Shake()
        {
            target.DOComplete(); 
            //will DOComplete other tween
            target.DOShakePosition(duration, amplitude).OnComplete(ResetPos);
        }

        void ResetPos()
        {
            target.transform.localPosition = _pos;
        }
    }
}