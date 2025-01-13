using System.Collections;
using UnityEngine;

public class SwordInStoneBehavior : MonoBehaviour
{
    Animator animator;
    public GameObject Neko;
    public Camera Camera;
    public SIS_JKey JKey;
    bool canTryToGetSword = false;

    //拔剑变量
    int tapCounter = 0;
    int tapTargetCount = 13;
    bool isChecking = false;
    bool canFirstCheck = true;
    bool checkSucceeded = false;
    float getCheckTimer = 0f;
    float getCheckDuration = 2.0f;

    //摄像机缩放变量
    bool cameraCanBackToInitial = false;
    float cameraInitialSize;
    float cameraCurrentSize;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        CameraInitialSet();//初始化相机
    }

    void Update()
    {
        if (canTryToGetSword)
            TapKeyCheck();
    }

    /// <summary>
    /// Neko进出碰撞箱逻辑
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == Neko && !checkSucceeded)
        {
            canTryToGetSword = true;
            JKey.seeable = true;
            JKey.canSelfRotate = true;
            JKey.canSelfScale = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == Neko)
        {
            canTryToGetSword = false;
            JKey.seeable = false;
            JKey.canSelfRotate = false;
            JKey.canSelfScale = false;
            if (isChecking)
                FailToGetSword();
        }
    }

    void TapKeyCheck()
    {
        if (Input.GetKeyDown(KeyCode.J) && canFirstCheck && !checkSucceeded)
        {
            isChecking = true;
            canFirstCheck = false;
        }//如果按下J并且可以首次，就进入检查

        if (isChecking)
        {
            IsGettingSword();
            getCheckTimer += Time.deltaTime;

            if (getCheckTimer >= getCheckDuration)
                FailToGetSword();

            if (Input.GetKeyDown(KeyCode.J) && !canFirstCheck)
            {
                tapCounter++;
                if (tapCounter >= tapTargetCount)
                    SwordIsGot();
            }
        }//如果是在检查状态，再按J就可以计数
    }

    void IsGettingSword()
    {
        cameraCanBackToInitial = false;
        animator.SetBool("isGetting", true);
        CameraTapScale();
        JKey.canSelfRotate = false;
        JKey.canSelfScale = false;
        JKey.isTappingKey = true;
    }//影响动画和相机

    void FailToGetSword()
    {
        cameraCanBackToInitial = true;
        getCheckTimer = 0;
        tapCounter = 0;
        isChecking = false;
        animator.SetBool("isGetting", false);
        JKey.canSelfRotate = true;
        JKey.canSelfScale = true;
        JKey.isTappingKey = false;
        StartCoroutine(CameraTryBackToInitialCoroutine());
        StartCoroutine(FailedTimerCoroutine(1.5f));
    }//重置变量，影响相机

    IEnumerator FailedTimerCoroutine(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        canFirstCheck = true;
    }//协程，一段时间后才可以再次拔剑

    void SwordIsGot()
    {
        canFirstCheck = false;
        cameraCanBackToInitial = true;
        checkSucceeded = true;
        animator.SetTrigger("got");
        JKey.seeable = false;
        StartCoroutine(CameraTryBackToInitialCoroutine());
    }//拔剑成功，设定变量

    void CameraInitialSet()
    {
        cameraInitialSize = Camera.orthographicSize;
        cameraCurrentSize = cameraInitialSize;
    }//相机初始尺寸记录和设置

    void CameraTapScale()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            StartCoroutine(CameraTapScaleCoroutine(0));
        }
    }//按J缩放相机

    IEnumerator CameraTapScaleCoroutine(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        cameraCurrentSize *= 0.97f;
        Camera.orthographicSize = cameraCurrentSize;
    }//缩放相机的协程

    IEnumerator CameraTryBackToInitialCoroutine()
    {
        if (cameraCanBackToInitial)
        {
            if (checkSucceeded)
                yield return new WaitForSeconds(1.5f);

            Camera.orthographicSize = cameraInitialSize;
            cameraCurrentSize = cameraInitialSize;
        }
    }//相机归位的协程
}