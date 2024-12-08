using System.IO;
using UnityEngine;

public class ScreenshotTaker : MonoBehaviour
{
    public int i = 1;

    private void Update()
    {
        // 检查是否按下了F10键
        if (Input.GetKeyDown(KeyCode.F10))
        {
            // 调用截图方法
            TakeScreenshot();
        }
    }

    private void TakeScreenshot()
    {

        // 保存截图到磁盘
        string path = System.IO.Path.Combine(Application.persistentDataPath, "Screenshot" + (i++) + ".png");
        ScreenCapture.CaptureScreenshot(path);
        Debug.Log("Screenshot saved: " + path);
    }
}