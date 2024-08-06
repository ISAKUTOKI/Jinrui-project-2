using UnityEngine;

namespace com
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform targetTrans;
        public Transform selfTrans;
         Vector3 _offset;
        public float followLerpFactor = 0.1f;
        public Vector3 offset;
         Vector3 posLastClipping;

        void Update()
        {
            selfTrans.position = Vector3.Lerp(selfTrans.position, targetTrans.position + offset, followLerpFactor);
        }

        private void Start()
        {
            posLastClipping = selfTrans.position;
        }
    }
}