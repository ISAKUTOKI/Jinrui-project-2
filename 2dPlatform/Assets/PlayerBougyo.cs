using Assets.Game.Script.HUD_interface.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBougyo : MonoBehaviour
{
    // Start is called before the first frame update

    public WeaponPowerSystem WPS;
    public float canBougyoValue = 0.1f;
    private bool canBougyo = false;
    private bool inBougyo = false;

    void Start()
    {
        //Debug.Log(WPS.power);
    }

    // Update is called once per frame
    void Update()
    {
        //更新能否防御的状态
        CanBougyo();

        //防御输入
        BougyoCheck();

        if (Input.GetKeyDown(KeyCode.F12))
            Debug.Log(WPS.power);
    }

    void CanBougyo()
    {
        if (WPS.power >= canBougyoValue)
        {
            canBougyo = true;
        }
        else
        {
            canBougyo = false;
        }
    }

    void BougyoCheck()
    {
        //按下K并且可以防御并且不在防御那就进入防御
        if (Input.GetKeyDown(KeyCode.K) && canBougyo && inBougyo == false)
        {
            PlayerBehaviour.instance.animator.SetTrigger("bougyo_start");
            PlayerBehaviour.instance.animator.SetBool("bougyo_cyuu", true);
            //debug开始防御
            Debug.Log("bougyo start");
            //防御中
            inBougyo = true;
        }

        //在防御中并且松开了k键就停止防御
        if (Input.GetKeyUp(KeyCode.K) && canBougyo && inBougyo == true || canBougyo == false)
        {
            PlayerBehaviour.instance.animator.SetTrigger("bougyo_out");
            PlayerBehaviour.instance.animator.SetBool("bougyo_cyuu", false);
            Debug.Log("bougyo out");
            inBougyo = false;
        }

    }

    ///弹反系统
    //在bougyo_start阶段，如果防御到了canDeflect的攻击，就弹反
    //弹反是立即在当前帧停顿，并且播放弹反到了的特效
    //如果没反应就继续进行防御动作
    //如果有按下攻击键就直接进入攻击动画



}
