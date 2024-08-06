using UnityEngine;

namespace com
{
    public class Billboard : MonoBehaviour
    {
        public Transform cameraTrans;
        public bool startOnly;

        void Start()
        {
            if (cameraTrans == null)
                cameraTrans = Camera.main.transform;

            if (startOnly)
                Set();
        }

        // Update is called once per frame
        void Update()
        {
            if (!startOnly)
                Set();
        }

        void Set()
        {
            transform.forward = cameraTrans.forward;
        }
    }
}