using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordInStoneBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    public Object Neko;
    bool canTryToGetSword = false;

    //这下面都是用于检测拔剑的变量
    int tapCounter = 0;
    int tapTargetCount = 10;
    bool isChecking = false;
    bool canFirstCheck = true;
    bool checkSucceeded = false;
    float getCheckTimer = 0f;
    float getCheckDuration = 2.0f;
    float giveUpCheckTimer = 0f;
    float giveUpCheckDuration = 0.5f;



    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        //ColliderCheck();
    }

    // Update is called once per frame
    void Update()
    {
        TryToGetSword();
    }



    void TryToGetSword()
    {
        if (canTryToGetSword)
        {
            HintKeyAppear();
            TapKeyCheck();
        }
    }
    void HintKeyAppear()
    {

    }//出现提示图标（未完成）


    /// <summary>
    /// 检测是否在拔剑及其结果
    /// </summary>
    void TapKeyCheck()
    {
        if (Input.GetKeyDown(KeyCode.J) && canFirstCheck && !checkSucceeded)
        {
            isChecking = true;
            canFirstCheck = false;
            Debug.Log("首次检查");
        }//第一次按下J，并且可以进行第一次检查，则可以开始检查条件，并且不可再进行第一次检查
        if (isChecking)
        {
            IsGettingSword();
            GetTimeCheck();
            GetCountCheck();
            //Debug.Log("isChecking is:" + isChecking);
        }//如果是正在进行检查，那么就调用计时器和计数器
    }//检测是否拔剑
    void GetTimeCheck()
    {
        //checkTimer = Time.deltaTime;
        getCheckTimer += Time.deltaTime;
        if (getCheckTimer >= getCheckDuration)//如果倒计时结束就失败
        {
            FailToGetSword();
        }
    }//拔剑计时器
    void GetCountCheck()
    {
        //GiveUpGetCheck();
        Debug.Log("次数检查");
        if (Input.GetKeyDown(KeyCode.J) && !canFirstCheck)
        {
            tapCounter++;
            //Debug.Log($"{tapCounter}");
        }
        if (tapCounter >= tapTargetCount)//如果成功达到了次数
        {
            Debug.Log("succeed to get sword");
            SwordIsGot();
            canFirstCheck = false;
            checkSucceeded = true;
        }
    }//拔剑计数器
    void GiveUpGetCheck()
    {
        ///
        ///尚未完成，逻辑有误，还需修正
        ///
        giveUpCheckTimer += Time.deltaTime;
        if (giveUpCheckTimer >= giveUpCheckDuration)//如果倒计时结束就失败
        {
            FailToGetSword();
        }
    }//拔剑放手计时器（未完成）
    void IsGettingSword()
    {
        animator.SetBool("isGetting", true);
    }//拔剑中
    void FailToGetSword()
    {
        //那么就退出检查状态，并且可以重新进行第一次检查
        getCheckTimer = 0;
        tapCounter = 0;
        isChecking = false;
        canFirstCheck = true;
        animator.SetBool("isGetting", false);
        Debug.Log("fail to get sword");
    }//拔剑失败
    void SwordIsGot()
    {
        animator.SetTrigger("got");
    }//拔剑成功


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
            Debug.Log("canGetSword is: " + canTryToGetSword);
        }
    }//进入
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == Neko)
        {
            canTryToGetSword = false;
            Debug.Log("canGetSword is:" + canTryToGetSword);
        }
    }//退出

}
