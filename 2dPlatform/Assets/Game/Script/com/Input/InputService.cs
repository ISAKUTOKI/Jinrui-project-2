using UnityEngine;

namespace com
{
    public class InputService : MonoBehaviour
    {
        public static InputService Instance;
        private void Awake()
        {
            Instance = this;
        }

        void StartListenCoreInput()
        {
           // Input.
           //     UnityEngine.UI.Button
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Insert))
            {
                var filename = "sc_"+Time.time.GetHashCode();
                ScreenCapture.CaptureScreenshot(Application.dataPath + "/" + filename + ".png");
            }
        }
    }
}
