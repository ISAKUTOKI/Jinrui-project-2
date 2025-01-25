using Assets.Game.Script.HUD_interface.Combat;
using com;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeflectBehaviour : MonoBehaviour
{
    public float deflectStartTime { get; private set; }

    [HideInInspector] public bool canDeflect;
    //弹反和防御系统
    /*
设计目的
	游戏的核心机制，对抗敌人，人物的攻击比较弱，所以要通过弹反提高对敌人的伤害能力
	同时也提供了防御的能力
	
	（秘密：更像是只狼的敌人）

操作
	按下某个键（比如k），如果玩家处于idle run状态就可以弹反，攻击的最后几帧也可以
    松开按键可以主动结束防御
动画
    开始弹反，先播放【弹反开始】动画，然后播放【防御】动画
	【弹反开始】动画期间，松开按键，依旧会播放完【弹反开始】动画
	【防御】动画期间，松开按键，结束播放回到idle动画
弹反流程
	伤害来源在判定框内时，如果处于弹反状态，
	弹反成功时，会播放【弹反成功】特效，
    抵消本次攻击，加1/3点能量
    此时按下攻击按键可以进入加强版攻击动画
    否则继续结算防御
防御流程
    伤害来源在判定框内时，如果处于防御非弹反状态，
    则消耗1点能量
    抵消本次攻击，
    被击退
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
    public bool deflectedThisMovement { get; private set; }

    private bool _isDefending;

    private void Awake()
    {
        deflectedThisMovement = false;
    }

    private void Update()
    {
        if (canDeflect)
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

            if (_isDefending)
            {
                var animator = PlayerBehaviour.instance.animator;
                if (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "bougyo")
                {
                    WeaponPowerSystem.instance.OnDefending();
                }
            }
        }
    }

    public bool currentCharacterStateAllowDeflect
    {
        get
        {
            //按下k就进入防御开始动作
            //attack(刚开始几帧不可以) wound jump(must grounded)
            //walk interrupt walk
            bool isAttacking = PlayerBehaviour.instance.attack.isAttacking;
            bool isAttackingButInCanDeflectState = PlayerBehaviour.instance.attack.isAttackingButCanInterrupt;
            bool isWounding = PlayerBehaviour.instance.health.isWounding;
            bool isGrounded = !PlayerBehaviour.instance.jump.IsJumping;
            //bool isWalking = false;
            Debug.Log(isAttacking + "" + isAttackingButInCanDeflectState);
            return (!isAttacking || isAttackingButInCanDeflectState)
                && isGrounded
                && !isWounding;
        }
    }
    void TryDefend()
    {
        if (!currentCharacterStateAllowDeflect)
            return;
        if (_isDefending)
            return;

        EnterDefend();
    }

    void TryStopDefend()
    {
        if (!_isDefending)
            return;

        ExitDefend();
    }

    void EnterDefend()
    {
        //perform deflect
        //stop walking
        //有能量就进入防御持续
        Debug.Log("bougyo_start");
        PlayerBehaviour.instance.animator.ResetTrigger("bougyo_out");
        PlayerBehaviour.instance.animator.SetTrigger("bougyo_start");
        _isDefending = true;
        deflectArea.enabled = true;
        PlayerBehaviour.instance.weaponView.SetState(PlayerWeaponView.State.hide);
        deflectedThisMovement = false;
    }
    public void OnWound()
    {
        //Debug.Log("OnWound");
        ExitDefend(false);
    }

    public void ExitDefend(bool withAnim = true)
    {
        //没能量就进入防御退出
        _isDefending = false;
        deflectArea.enabled = false;
        deflectedThisMovement = false;
        PlayerBehaviour.instance.animator.ResetTrigger("bougyo_start");
        if (withAnim)
            PlayerBehaviour.instance.animator.SetTrigger("bougyo_out");
        //PlayerBehaviour.instance.weaponView.SetState(PlayerWeaponView.State.idle);
        ////Debug.Log("ExitDefend！ withAnim " + withAnim);
    }

    public bool isDefending
    {
        get
        {
            if (_isDefending)
            {
                return true;
            }
            var animator = PlayerBehaviour.instance.animator;
            var infos = animator.GetCurrentAnimatorClipInfo(0);
            if (infos.Length < 1)
            {
                return false;
            }
            var n = infos[0].clip.name;
            if (n == "bougyo_start" || n == "bougyo")
            {
                return true;
            }
            return false;
        }
    }

    public bool isDeflecting
    {
        get
        {
            var animator = PlayerBehaviour.instance.animator;
            var infos = animator.GetCurrentAnimatorClipInfo(0);
            if (infos.Length < 1)
            {
                return false;
            }
            if (infos[0].clip.name == "bougyo_start")
            {
                return true;
            }
            return false;
        }
    }

    public bool isInDefendEnd
    {
        get
        {
            var animator = PlayerBehaviour.instance.animator;
            var infos = animator.GetCurrentAnimatorClipInfo(0);
            if (infos.Length < 1)
            {
                return false;
            }
            if (infos[0].clip.name == "bougyo_end")
            {
                return true;
            }
            return false;
        }
    }

    public bool isInNoMoveState
    {
        get
        {
            return isDefending || isInDefendEnd || isDeflecting;
        }
    }
    public void TriggerDeflect(bool isSuperToGainFullPower = false)
    {
        Debug.Log("TriggerDeflect");
        deflectedThisMovement = true;
        if (isSuperToGainFullPower)
        {
            // 立即在当前帧停顿，并插入超大弹反特效
            WeaponPowerSystem.instance.GainFullPower();
            //Instantiate(deflectEffectPrefab, deflectEffectPosition, Quaternion.identity);
        }
        else
        {
            // 立即在当前帧停顿，并插入弹反特效
            WeaponPowerSystem.instance.GainOnePower();
            //Instantiate(deflectEffectPrefab, deflectEffectPosition, Quaternion.identity);
        }
        CreateSpriteVfxSystem.instance.Create("Firelight2", deflectArea.transform.position);
    }

    public void TriggerDefend(bool isToRight)
    {
        Debug.Log("TriggerDefend isToRight " + isToRight);
        //击退
        if (isToRight)
            PlayerBehaviour.instance.move.knockback.KnockBackToRight(1);
        else
            PlayerBehaviour.instance.move.knockback.KnockBackToLeft(1);

        WeaponPowerSystem.instance.ConsumePower_cell(1);
        CreateSpriteVfxSystem.instance.Create("Firelight1", deflectArea.transform.position);
    }

    // 弹反后进入攻击动画
    public void DeflectToAttack()
    {
        // 如果按下攻击键，使用 deflect_to_attack 的 Trigger 进入攻击动画
        PlayerBehaviour.instance.animator.SetTrigger("deflect_to_attack");
    }
}