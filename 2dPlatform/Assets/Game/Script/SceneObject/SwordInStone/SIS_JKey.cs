using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SIS_JKey : MonoBehaviour
{
    public GameObject JKey;
    public SpriteRenderer JKeyRenderer;

    [HideInInspector] public bool seeable = false;
    [HideInInspector] public bool canSelfRotate = false;
    [HideInInspector] public bool canSelfScale = false;
    [HideInInspector] public bool isTappingKey = false;
    [HideInInspector] public bool isTapScaling = false;

    //以下为旋转用的变量
    private float rotationSpeed = 3.0f; // 旋转速度
    private float maxRotationAngle = 2.0f; // 最大旋转角度
    private bool rotateClockwise = true; // 旋转方向，true为顺时针，false为逆时针
    private float currentRotationAngle = 0.0f; // 当前旋转角度

    //以下为缩放用的变量
    private float scaleSpeed = 1.8f; // 缩放速度
    private float maxScale = 1.3f; // 最大缩放比例
    private float minScale = 1.0f; // 最小缩放比例
    private bool isScalingUp = true; // 缩放方向，true为放大，false为缩小
    private Vector3 originalScale; // 物体的原始缩放比例

    void Start()
    {
        originalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        HintKeyVisibility();
        if (canSelfRotate)
            SelfRotate();
        if (canSelfScale)
            SelfScale();
        if (isTappingKey)
            TappingKey();
    }


    void HintKeyVisibility()
    {
        if (seeable)
            JKeyRenderer.enabled = true;
        if (!seeable)
            JKeyRenderer.enabled = false;
    }
    void SelfRotate()
    {
        if (rotateClockwise)
        {
            currentRotationAngle += rotationSpeed * Time.deltaTime;
            if (currentRotationAngle >= maxRotationAngle)
            {
                rotateClockwise = false; // 达到最大角度，改变旋转方向
            }
        }
        else
        {
            currentRotationAngle -= rotationSpeed * Time.deltaTime;
            if (currentRotationAngle <= -maxRotationAngle)
            {
                rotateClockwise = true; // 达到最小角度，改变旋转方向
            }
        }
        // 应用旋转到物体
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, currentRotationAngle);
    }
    void SelfScale()
    {
        // 根据缩放方向更新当前缩放比例
        if (isScalingUp)
        {
            // 逐步放大
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale * maxScale, scaleSpeed * Time.deltaTime);
            // 检查是否达到最大缩放比例
            if (transform.localScale.x >= originalScale.x * maxScale - 0.1f)
            {
                isScalingUp = false; // 改变缩放方向为缩小
            }
        }
        else
        {
            // 逐步缩小
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale * minScale, scaleSpeed * Time.deltaTime);
            // 检查是否达到最小缩放比例
            if (transform.localScale.x <= originalScale.x * minScale + 0.1f)
            {
                isScalingUp = true; // 改变缩放方向为放大
            }
        }
    }

    void TappingKey()
    {
        if (isTappingKey && Input.GetKeyDown(KeyCode.J) && !isTapScaling) // 检查是否按下J键并且当前没有缩放操作在进行
        {
            StartCoroutine(ScaleJKey());
            //Debug.Log("OK");
        }
    }
    IEnumerator ScaleJKey()
    {
        isTapScaling = true; // 开始缩放操作，设置标志为true

        yield return StartCoroutine(ScaleOverTime(1.3f, 0.1f));

        yield return StartCoroutine(ScaleOverTime(0.7f, 0.1f));

        isTapScaling = false; // 缩放操作完成，设置标志为false
    }//控制缩放的协程
    IEnumerator ScaleOverTime(float targetScale, float duration)
    {
        float startTime = Time.time;
        Vector3 originalScale = transform.localScale;

        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;
            transform.localScale = Vector3.Lerp(originalScale, originalScale * targetScale, t);
            yield return null;
        }

        transform.localScale = originalScale * targetScale;
    }//进行缩放动作的协程
}
