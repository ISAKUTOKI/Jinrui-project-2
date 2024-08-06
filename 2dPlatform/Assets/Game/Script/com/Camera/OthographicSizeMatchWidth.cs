using UnityEngine;

namespace com
{
    public class OthographicSizeMatchWidth : MonoBehaviour
    {
        public Camera cam;
        public float originalHeight;
        public float originalWidth;
        public float sizeMax = 14;
        public float sizeMin = 8;
        public float sizeBase = 12;

        public bool matchOnEnable;

        void OnEnable()
        {
            if (matchOnEnable)
            {
                Match();
            }
        }

        public void Match()
        {
            cam.orthographicSize = GetGoodSize();
        }

        public float GetGoodSize()
        {
            float w = Screen.width;
            float h = Screen.height;
            //Debug.Log("w " + w);
            //Debug.Log("h " + h);

            float ratio = w / h;
            float perferredRatio = originalWidth / originalHeight;
            var size = sizeBase;
            if (ratio <= perferredRatio)
            {
                //Debug.Log("narrow screen");
                size = sizeBase * perferredRatio / ratio;
            }
            else
            {
                size = sizeBase / perferredRatio * ratio;
            }
            size = Mathf.Max(sizeMin, Mathf.Min(size, sizeMax));

            return size;
        }
    }
}