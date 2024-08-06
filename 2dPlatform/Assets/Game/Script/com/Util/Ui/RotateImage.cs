using UnityEngine;

namespace com
{
    public class RotateImage : MonoBehaviour
    {
        public RectTransform trans;
        public bool useRawTime = true;
        public Vector3 Speed = new Vector3(0, 0, 1);

        void Start()
        {
            if (trans == null)
                trans = GetComponent<RectTransform>();
        }

        private void Update()
        {
            trans.localEulerAngles += Speed * (useRawTime ? Time.deltaTime : GameTime.deltaTime);
        }
    }
}
