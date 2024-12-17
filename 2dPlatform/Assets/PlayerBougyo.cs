using Assets.Game.Script.HUD_interface.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBougyo : MonoBehaviour
{
    // Start is called before the first frame update

    public WeaponPowerSystem WPS;
    public deflectArea deflectArea;
    public float canBougyoValue = 0.1f;
    private bool canBougyo = false;
    private bool inBougyo = false;
    private bool canDeflect = false;

    void Start()
    {
        //Debug.Log(WPS.power);
    }

    // Update is called once per frame
    void Update()
    {
        //更新能否防御的状态
        CanBougyoSwitch();

        //防御输入
        BougyoCheck();

        if (Input.GetKeyDown(KeyCode.F12))
            Debug.Log(WPS.power);
    }

    void CanBougyoSwitch()
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
    //在bougyo_start动画的动画事件DeflectCheckStart和DeflectCheckEnd的中间
    //如果防御到了canBeDeflected的攻击，就弹反
    //否则就pass
    //弹反是立即在当前帧停顿，并且插入弹反到了的特效
    //如果没反应就继续进行防御动作
    //如果有按下攻击键就直接用deflect_to_attack的Trigger进入攻击动画

    public void DeflectCheckStart()
    {
        // 检查是否可以弹反
        CheckForDeflection();
    }

    // 动画事件：结束检查弹反
    public void DeflectCheckEnd()
    {
        // 如果可以弹反，执行弹反逻辑
        if (canDeflect)
        {
            // 立即在当前帧停顿，并插入弹反特效
            PlayerBehaviour.instance.animator.SetTrigger("deflect");
            // 播放弹反特效
            // 例如：Instantiate(deflectEffectPrefab, deflectEffectPosition, Quaternion.identity);
        }
        else
        {
            // 如果没有弹反成功，继续执行防御动作
            // 无需额外代码，因为防御动作已经在Animator中设置
        }
    }

    // 检查是否可以弹反
    private void CheckForDeflection()
    {
        // 这里添加检查弹反的逻辑
        // 例如：检测是否有敌人攻击并与防御时间匹配
        // 如果弹反成功，设置 canDeflect 为 true
        // 注意：这里的逻辑需要根据你的游戏实际情况来编写


        //foreach (var p in deflectArea.targets)
        //{
        //    if (p == null)
        //    {
        //        continue;
        //    }
        //    p.Reverse();
        //}
    }

    // 弹反后进入攻击动画
    public void DeflectToAttack()
    {
        // 如果按下攻击键，使用 deflect_to_attack 的 Trigger 进入攻击动画
        if (Input.GetKeyDown(KeyCode.J))
        {
            PlayerBehaviour.instance.animator.SetTrigger("deflect_to_attack");
        }
    }


}
