using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwordInStoneBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    public GameObject Neko;
    public Camera Camera;
    public SIS_JKey JKey;
    bool canTryToGetSword = false;

    //以下为用于检测拔剑的变量
    int tapCounter = 0;
    int tapTargetCount = 13;
    bool isChecking = false;
    bool canFirstCheck = true;
    bool checkSucceeded = false;
    float getCheckTimer = 0f;
    float getCheckDuration = 2.0f;

    //以下为相机用变量
    bool cameraCanBackToInitial = false;
    float cameraInitialSize;
    float cameraCurrentSize;
    float cameraScaleWaitTimer = 0f;
    float cameraScaleWaitDuration = 1.5f;


    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        CameraInitialSet();
        //ColliderCheck();
    }

    void Update()
    {
        if (canTryToGetSword)
            TapKeyCheck();
    }



    /// <summary>
    /// 检查Neko是否进入碰撞箱
    /// </summary>
    void ColliderCheck()
    {
        Collider2D collider = GetComponent<BoxCollider2D>();
        if (collider != null)
            Debug.Log("collider " + collider + " is got");
        else
            Debug.Log("FAILED");
    }//检查是否存在
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == Neko)
        {
            canTryToGetSword = true;
            JKey.seeable = true;
            JKey.canSelfRotate = true;
            JKey.canSelfScale = true;
            //Debug.Log("canGetSword is: " + canTryToGetSword);
        }
    }//进入
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == Neko)
        {
            canTryToGetSword = false;
            JKey.seeable = false;
            JKey.canSelfRotate = false;
            JKey.canSelfScale = false;
        }
    }//退出

    /// <summary>
    /// 检测是否在拔剑及其结果
    /// </summary>
    void TapKeyCheck()
    {
        if (Input.GetKeyDown(KeyCode.J) && canFirstCheck && !checkSucceeded)
        {
            isChecking = true;
            canFirstCheck = false;
            //Debug.Log("首次检查");
        }//第一次按下J，并且可以进行第一次检查，则可以开始检查条件，并且不可再进行第一次检查
        if (isChecking)
        {
            IsGettingSword();
            GetTimeCheck();
            GetCountCheck();
            CameraTryBackToInitial();
            //Debug.Log("isChecking is:" + isChecking);
        }//如果是正在进行检查，那么就调用计时器和计数器
    }//检测是否拔剑
    void GetTimeCheck()
    {
        getCheckTimer += Time.deltaTime;
        if (getCheckTimer >= getCheckDuration)//如果倒计时结束就失败
        {
            FailToGetSword();
        }
    }//拔剑计时器
    void GetCountCheck()
    {
        //GiveUpGetCheck();
        //Debug.Log("次数检查");
        if (Input.GetKeyDown(KeyCode.J) && !canFirstCheck)
        {
            tapCounter++;
            //GiveUpToGetCheck();
            //Debug.Log(giveUpCheckTimer);
        }
        if (tapCounter >= tapTargetCount)//如果成功达到了次数
        {
            SwordIsGot();
        }
    }//拔剑计数器
    void IsGettingSword()
    {
        cameraCanBackToInitial=false;
        animator.SetBool("isGetting", true);
        CameraTapScale();
        JKey.canSelfRotate = false;
        JKey.canSelfScale = false;
        JKey.isTappingKey = true;
    }//拔剑中
    void FailToGetSword()
    {
        //那么就退出检查状态，并且重置状态
        //giveUpCheckTimer = 0;
        cameraCanBackToInitial = true;
        getCheckTimer = 0;
        tapCounter = 0;
        isChecking = false;
        animator.SetBool("isGetting", false);
        //Debug.Log("失败");
        JKey.canSelfRotate = true;
        JKey.canSelfScale = true;
        JKey.isTappingKey = false;
        StartCoroutine(FailedTimerCoroutine(1.5f));
    }//拔剑失败
    IEnumerator FailedTimerCoroutine(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        canFirstCheck = true;
    }//等待一小会才能再次拔剑
    void SwordIsGot()
    {
        canFirstCheck = false;
        cameraCanBackToInitial = true;
        checkSucceeded = true;
        animator.SetTrigger("got");
        //Debug.Log("拔出来了");
        JKey.seeable = false;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }//拔剑成功


    /// <summary>
    /// 各种其他效果
    /// </summary>
    void CameraInitialSet()
    {
        cameraInitialSize = Camera.orthographicSize;
        cameraCurrentSize = cameraInitialSize;
    }//相机数值初始化
    void CameraTapScale()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            cameraCurrentSize = cameraCurrentSize * 0.97f;
            Camera.orthographicSize = cameraCurrentSize;
        }
    }//按下按键的时候进行一次镜头缩放
    void CameraTryBackToInitial()
    {
        if (cameraCanBackToInitial && checkSucceeded)
        {
            cameraScaleWaitTimer += Time.deltaTime;//计时开始
            if (cameraScaleWaitTimer >= cameraScaleWaitDuration)
            {
                Camera.orthographicSize = Mathf.Lerp(Camera.orthographicSize, cameraInitialSize, Time.deltaTime * 2.0f);
                cameraCurrentSize = cameraInitialSize;
            }
            return;
        }
        if (cameraCanBackToInitial)
        {
            Camera.orthographicSize = Mathf.Lerp(Camera.orthographicSize, cameraInitialSize, Time.deltaTime * 1.0f);
            cameraCurrentSize = cameraInitialSize;
            //Debug.Log("调用了");
        }


    }//镜头归位

}
