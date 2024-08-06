using UnityEngine;
using com;

public class PauseSystem : MonoBehaviour
{
    public static PauseSystem instance { get; private set; }

    public int w;
    public int h;
    private void Awake()
    {
        instance = this;
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            ScreenshotHandler.TakeScreenshot_Static(w, h);
        }
    }
}