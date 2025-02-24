using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCameraCtrl : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    float currentSize;
    Coroutine currentCoroutine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(CameraSizeScale(5.0f));
            //Debug.Log("玩家进入");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(CameraSizeScale(currentSize));
            //Debug.Log("玩家退出");
        }
    }

    private IEnumerator CameraSizeScale(float targetSize)
    {
        currentSize = mainCamera.orthographicSize;
        while (Mathf.Abs(mainCamera.orthographicSize - targetSize) > 0.01f)
        {
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, targetSize, 0.02f);
            yield return null;
        }
        mainCamera.orthographicSize = targetSize;
    }
}
