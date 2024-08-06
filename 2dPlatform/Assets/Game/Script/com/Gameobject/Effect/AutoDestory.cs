using UnityEngine;

namespace com
{
    public class AutoDestory : MonoBehaviour
    {
        public float killTime = 2;

        private void Start()
        {
            Destroy(gameObject, killTime);
        }
    }
}
