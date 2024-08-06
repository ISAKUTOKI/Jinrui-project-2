using UnityEngine;
using Text = TMPro.TextMeshProUGUI;

namespace com
{
    public class BlinkTipBehaviour : MonoBehaviour
    {
        public CanvasGroup Cg;
        public BlinkCanvasGroup Bcg;
        public Text text;
        private System.Func<bool> _endReason;

        public void Show(string s)
        {
            text.text = s;
            Cg.alpha = 1;
            Bcg.enabled = true;
            _endReason = null;
        }

        public void Show(string s, System.Func<bool> endReason)
        {
            Show(s);
            _endReason = endReason;
        }

        public void OnLoopStart()
        {
            if (_endReason != null)
            {
                var res = _endReason.Invoke();
                if (res)
                    Hide(true);
            }
        }

        public void Hide(bool clearEndReaon)
        {
            Cg.alpha = 0;
            Bcg.enabled = false;
            if (clearEndReaon)
                _endReason = null;
        }
    }
}