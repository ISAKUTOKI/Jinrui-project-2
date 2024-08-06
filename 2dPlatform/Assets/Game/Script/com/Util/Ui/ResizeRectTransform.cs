using UnityEngine;
using Text = TMPro.TextMeshProUGUI;
using UnityEngine.UI;

namespace com
{
    public class ResizeRectTransform : MonoBehaviour
    {
        public RectTransform target;
        public RectTransform reference;
        public RectTransform reference2;
        public Text referenceOverrideText;
        public bool resizeHeight;
        public bool resizeWidth;
        public float heightOffset;
        public float widthOffset;
        public bool toResize = false;

        public ContentSizeFitter[] csfForceReset;
        public LayoutGroup[] lgForceReset;
        int _waitFrames;
        bool _toStep2;

        public void Update()
        {
            if (toResize)
            {
                if (_waitFrames > 0)
                {
                    _waitFrames--;
                    return;
                }

                toResize = false;
                Resize();
            }
            else
            {
                if (_toStep2)
                    Resize_step2();
            }
        }

        public void ResizeLater(int wf = 0)
        {
            toResize = true;
            _toStep2 = false;
            _waitFrames = wf;
            //Debug.Log("ResizeLater");
        }

        void Resize()
        {
            if (csfForceReset != null)
            {
                foreach (var c in csfForceReset)
                {
                    c.enabled = false;
                    c.enabled = true;
                }
            }
            if (lgForceReset != null)
            {
                foreach (var c in lgForceReset)
                {
                    c.enabled = false;
                    c.enabled = true;
                }
            }
            _toStep2 = true;
        }

        void Resize_step2()
        {
            //Debug.Log("size");
            var size = target.sizeDelta;
            //Debug.Log(size);
            var sizeRef = reference.sizeDelta;
            //Debug.Log(sizeRef);
            if (referenceOverrideText != null)
            {
                //refText.autoSizeTextContainer = true;
                sizeRef.x = referenceOverrideText.renderedWidth;
                sizeRef.y = referenceOverrideText.renderedHeight;
                // Debug.Log(sizeRef);
            }
            if (reference2 != null)
            {
                sizeRef.x += reference2.sizeDelta.x;
                sizeRef.y += reference2.sizeDelta.y;
            }

            if (resizeWidth)
            {
                sizeRef.x = Mathf.Max(0, sizeRef.x);
                size.x = sizeRef.x + widthOffset;
            }

            if (resizeHeight)
            {
                sizeRef.y = Mathf.Max(0, sizeRef.y);
                size.y = sizeRef.y + heightOffset;
            }
            //Debug.Log(size);
            target.sizeDelta = size;
        }
    }
}