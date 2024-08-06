using UnityEngine;

namespace com
{
    public class CameraScalerWithTween : CameraScaler
    {
        public float tweenFactor = 0.99f;
        private Vector3 _targetPos;
        private float _targetOrthoSize;

        protected override void SetCam()
        {
            var bound = GetBound();
            _targetPos = GetCamPos(bound);
            _targetOrthoSize = GetCamOrthoSize(bound);
        }

        void FixedUpdate()
        {
            cam.transform.position = Vector3.Lerp(_targetPos, cam.transform.position, tweenFactor);
            cam.orthographicSize = Mathf.Lerp(_targetOrthoSize, _targetOrthoSize, tweenFactor);
        }
    }
}