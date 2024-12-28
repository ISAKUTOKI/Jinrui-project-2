using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SlowMoAndReloadScene : MonoBehaviour
{
    public float slowMoDuration = 5f; // 慢动作持续时间

    private void Start()
    {
        // 开始慢动作
        Time.timeScale = 1f; // 确保开始时Time.timeScale为1
        StartCoroutine(SlowMoAndReload());
    }

    private void Update()
    {
        Debug.Log("Time.timeScale: " + Time.timeScale);
    }

    IEnumerator SlowMoAndReload()
    {
        float timer = 0f;
        while (timer < slowMoDuration)
        {
            Time.timeScale = Mathf.Lerp(1f, 0f, timer / slowMoDuration); // 平滑减少Time.timeScale
            timer += Time.unscaledDeltaTime; // 使用unscaledDeltaTime以确保不受Time.timeScale影响
            yield return null;
        }

        // 确保Time.timeScale为0
        Time.timeScale = 0f;

        // 等待一帧时间，确保所有基于时间的操作都已停止
        yield return new WaitForEndOfFrame();

        // 重启当前场景
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}