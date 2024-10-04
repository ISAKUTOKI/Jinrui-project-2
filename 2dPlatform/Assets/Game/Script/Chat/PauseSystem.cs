using UnityEngine;
using com;

public class PauseSystem : MonoBehaviour
{
    public static PauseSystem instance { get; private set; }

    public int w;
    public int h;

    bool _isPaused;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        Resume();
    }

    public void Pause()
    {
        Time.timeScale = 0;
        ShowPauseUi();
        _isPaused = true;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        HidePauseUi();
        _isPaused = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            ScreenshotHandler.TakeScreenshot_Static(w, h);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (_isPaused)
                Resume();
            else
                Pause();
        }

    }

    [SerializeField] UiImageAnimation uiAnimation;
    [SerializeField] GameObject btns;
    [SerializeField] CanvasGroup cgUiAnimation;

    void ShowPauseUi()
    {
        cgUiAnimation.alpha = 1;
        cgUiAnimation.blocksRaycasts = true;
        cgUiAnimation.interactable = true;
        uiAnimation.Play(0);
        uiAnimation.SetPlayEndCallback(() => { btns.SetActive(true); });
    }

    void HidePauseUi()
    {
        cgUiAnimation.alpha = 0;
        cgUiAnimation.blocksRaycasts = false;
        cgUiAnimation.interactable = false;
        uiAnimation.Stop();
        btns.SetActive(false);
    }
}