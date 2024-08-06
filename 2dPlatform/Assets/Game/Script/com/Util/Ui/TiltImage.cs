using UnityEngine;

namespace com
{
    public class TiltImage : MonoBehaviour
    {
        public float freq;
        public RectTransform trans;
        public float amplitude;
        public Vector3 baseEular;
        public bool useRawTime = true;

        private void Start()
        {
            if (trans == null)
            {
                trans = GetComponent<RectTransform>();
            }
        }

        private void Update()
        {
            trans.localEulerAngles = baseEular + new Vector3(0, 0, amplitude) * Mathf.Sin((useRawTime ? Time.time : com.GameTime.time) * freq);
        }
    }
}
