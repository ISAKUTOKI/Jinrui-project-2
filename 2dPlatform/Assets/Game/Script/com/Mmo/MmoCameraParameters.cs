using UnityEngine;

namespace com
{
    [System.Serializable]
    public struct MmoCameraParameters
    {
        public Vector3 offset;

        [Range(0f, 1.5f)]
        public float pitch;
        [Range(-2f, 2f)]
        public float yaw;

        [Range(2f, 20f)]
        public float distance;
    }
}
