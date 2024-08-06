using UnityEngine;

namespace com
{
    public class SizeRandomnizer : MonoBehaviour
    {
        public Vector3 max;
        public Vector3 min;

        void Start()
        {
            transform.localScale = Vector3.Lerp(min, max, Random.value);
        }
    }
}