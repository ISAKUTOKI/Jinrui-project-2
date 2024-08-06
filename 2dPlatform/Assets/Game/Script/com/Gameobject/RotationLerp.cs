using UnityEngine;

namespace com
{
    public class RotationLerp : MonoBehaviour
    {
        public Vector3 rotation1;
        public Vector3 rotation2;
        public float speed;
        private float _timeOffset;

        void Start()
        {
            _timeOffset = Time.time;
        }

        void Update()
        {
            Vector3 v = Vector3.Lerp(rotation1, rotation2, Mathf.Cos((Time.time - _timeOffset) * speed) * 0.5f + 0.5f);
            transform.localEulerAngles = v;
        }
    }
}