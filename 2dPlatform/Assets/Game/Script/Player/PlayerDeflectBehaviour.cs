using Assets.Game.Script.HUD_interface.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeflectBehaviour : MonoBehaviour
{
    public float deflectStartTime { get; private set; }
    public ParticleSystem ps;

    public PlayerActionPerformDependency dependency;

    //弹反和防御系统
    /*
设计目的
	游戏的核心机制，对抗敌人，人物的攻击比较弱，所以要通过弹反提高对敌人的伤害能力
	同时也提供了防御的能力
	
	（秘密：更像是只狼的敌人）

操作
	按下某个键（比如k），如果玩家处于idle run状态就可以弹反，攻击的最后几帧也可以
    松开按键可以主动结束防御
流程
	开始弹反，先播放【弹反开始】动画，然后播放【防御】动画
	【弹反开始】动画期间，松开按键，依旧会播放完【弹反开始】动画
	【防御】动画期间，松开按键，结束播放回到idle动画
	弹反成功时，会播放【弹反成功】动画，结束播放回到idle动画

判定
	时机：【弹反开始】动画期间以及【防御】动画期间的前几帧，可以进行弹反
	位置：远程攻击的伤害本体在玩家面前，近战攻击的发起者在玩家面前

资源
	弹反可以积累【防御条】，防御会消耗【防御条】，【防御条】为0时无法防御（会直接结束防御）
	攻击时会消耗【防御条】并强化攻击，攻击速度大幅度提高
	会自然缓慢减少

UI
    一把刀的进度条

*/

    public DeflectAreaBehaviour deflectArea;
    public float defendMinEnergyRequirement = 0.15f;


    private void Awake()
    {

    }

    private void Update()
    {
        var keyCode = KeyCode.K;
        if (Input.GetKeyDown(keyCode))
        {
            TryDefend();
        }

        if (Input.GetKeyUp(keyCode))
        {
            TryStopDefend();
        }
    }

    public bool currentCharacterStateAllowDeflect
    {
        get
        {
            //按下k就进入防御开始动作
            //attack(刚开始几帧不可以) wound jump(must grounded)
            //walk interrupt walk
            bool isAttacking = false;
            bool isAttackingButInCanDeflectState = false;
            bool isWounding = false;
            bool isGrounded = true;
            //bool isWalking = false;
            return (!isAttacking || (isAttacking && isAttackingButInCanDeflectState))
                && isGrounded
                && !isWounding;
        }
    }
    void TryDefend()
    {
        //Debug.LogWarning("OnCheckCombo " + _comboOn);
        //PlayerBehaviour.instance.animator.ResetTrigger("combo");
        if (!currentCharacterStateAllowDeflect)
            return;
        if (isDefending)
            return;
        if (!DefendEnergyCheck())
            return;

        PerformDefend();
    }

    void TryStopDefend()
    {
        if (!isDefending)
            return;

        ExitDefend();
    }

    void PerformDefend()
    {
        //perform deflect
        //stop walking
        //有能量就进入防御持续
        Debug.Log("bougyo_start");
        PlayerBehaviour.instance.animator.ResetTrigger("bougyo_out");
        PlayerBehaviour.instance.animator.SetTrigger("bougyo_start");
        isDefending = true;
        deflectArea.enabled = true;
        PlayerBehaviour.instance.weaponView.SetState(PlayerWeaponView.State.hide);
    }
    public void OnWound()
    {
        Debug.Log("OnWound");
        ExitDefend(false);
    }

    public void ExitDefend(bool withAnim = true)
    {
        //没能量就进入防御退出
        isDefending = false;
        deflectArea.enabled = false;
        PlayerBehaviour.instance.animator.ResetTrigger("bougyo_start");
        if (withAnim)
            PlayerBehaviour.instance.animator.SetTrigger("bougyo_out");
        //PlayerBehaviour.instance.weaponView.SetState(PlayerWeaponView.State.idle);
        Debug.Log("ExitDefend！ withAnim " + withAnim);
    }


    bool DefendEnergyCheck()
    {
        return WeaponPowerSystem.instance.power >= defendMinEnergyRequirement;
    }

    public bool isDefending { get; private set; }

    public bool isDeflecting
    {
        get
        {
            if (!isDefending)
                return false;
            var animator = PlayerBehaviour.instance.animator;
            if (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "bougyo_start")
            {
                return true;
            }
            return false;
        }
    }


    public void OnDeflected(bool isSecondDamage)
    {
        // 立即在当前帧停顿，并插入弹反特效
        PlayerBehaviour.instance.animator.SetTrigger("deflect");
        // 播放弹反特效
        // 例如：Instantiate(deflectEffectPrefab, deflectEffectPosition, Quaternion.identity);
    }

    // 弹反后进入攻击动画
    public void DeflectToAttack()
    {
        // 如果按下攻击键，使用 deflect_to_attack 的 Trigger 进入攻击动画
        PlayerBehaviour.instance.animator.SetTrigger("deflect_to_attack");
    }
}