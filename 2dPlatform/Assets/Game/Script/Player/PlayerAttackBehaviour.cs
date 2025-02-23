using Assets.Game.Script.HUD_interface.Combat;
using System.Collections;
using UnityEngine;

public class PlayerAttackBehaviour : MonoBehaviour
{
    [HideInInspector] public bool canAttack;

    private PlayerHealthBehaviour _health;
    private PlayerMove _move;
    //public ParticleSystem ps;

    public AnimationClipData clip_swing1;
    public AnimationClipData clip_swing2;
    public AnimationClipData clip_swing3;

    private float clipDefaultSpeed_swing1;
    private float clipDefaultSpeed_swing2;
    private float clipDefaultSpeed_swing3;
    public float superAttackSwingSpeedMultiplier = 2;

    public Collider2D[] swing1_Cols;
    public Collider2D[] swing2_Cols;
    public Collider2D[] swing3_Cols;

    public int damage_swing1;
    public int damage_swing1_Super;
    public int damage_swing2;
    public int damage_swing2_Super;
    public int damage_swing3;
    public int damage_swing3_Super;
    private int _attackMovementPhase;//0 没有攻击 1~3 第1~3次斩击 
    public bool isSuperAttack;//弹反攻击
    //public PlayerActionPerformDependency dependency;
    private bool _comboOn;

    //public Transform damageOrigin;
    //public float damageRadius = 1.5f;


    private void Awake()
    {
        //_jump = GetComponent<PlayerJump>();
        _health = GetComponent<PlayerHealthBehaviour>();
        _move = GetComponent<PlayerMove>();
        SetAttackPhase(0);
        isSuperAttack = false;
    }

    public void Start()
    {
        //clipDefaultSpeed_swing1 = clip_swing1.speedRatio;
        //clipDefaultSpeed_swing2 = clip_swing2.speedRatio;
        //clipDefaultSpeed_swing3 = clip_swing3.speedRatio;
    }

    public int GetDamage(bool isSuper)
    {
        switch (_attackMovementPhase)
        {
            case 1:
                if (isSuper)
                    return damage_swing1_Super;
                return damage_swing1;
            case 2:
                if (isSuper)
                    return damage_swing2_Super;
                return damage_swing2;
            case 3:
                if (isSuper)
                    return damage_swing3_Super;
                return damage_swing3;
        }
        return 0;
    }


    void SyncSwingAnim(int phase)
    {
        //Debug.Log("SyncSwingAnim " + phase + " " + isSuperAttack);
        var f = isSuperAttack ? superAttackSwingSpeedMultiplier : 1;
        //PlayerBehaviour.instance.animator.SetBool("super", isSuperAttack);
        switch (phase)
        {
            case 1:
                PlayerBehaviour.instance.animator.SetFloat("attack1Speed", f);
                // PlayerBehaviour.instance.SetAnimatorSpeed(0, "attack1", clipDefaultSpeed_swing1 * f);
                break;
            case 2:
                PlayerBehaviour.instance.animator.SetFloat("attack2Speed", f);
                //  PlayerBehaviour.instance.SetAnimatorSpeed(0, "attack2", clipDefaultSpeed_swing2 * f);
                break;
            case 3:
                PlayerBehaviour.instance.animator.SetFloat("attack3Speed", f);
                // PlayerBehaviour.instance.SetAnimatorSpeed(0, "attack3", clipDefaultSpeed_swing3 * f);
                break;
        }
    }

    private void Update()
    {
        if (canAttack)
        {
            CheckStopAttack();
            CheckAttack();
        }
    }

    /// <summary>
    /// 攻击中，但是处于攻击的前几帧，可以用弹反打断的时间段
    /// </summary>
    /// <returns></returns>
    public bool isAttackingButCanInterrupt
    {
        get
        {
            var clipInfos = PlayerBehaviour.instance.animator.GetCurrentAnimatorClipInfo(0);
            if (clipInfos.Length < 1)
            {
                return false;
            }

            var clipInfo = clipInfos[0];
            var clip = clipInfo.clip;
            if (clip == clip_swing1.clip || clip == clip_swing2.clip || clip == clip_swing3.clip)
            {
                var stateInfo = PlayerBehaviour.instance.animator.GetCurrentAnimatorStateInfo(0);
                float normalizedTime = stateInfo.normalizedTime;
                float clipLength = stateInfo.length;
                float playbackTime = normalizedTime * clipLength;
                Debug.Log("isAttackingButCanInterrupt clip " + clip.name + " playbackTime " + playbackTime);
                return playbackTime < 0.35f;
            }

            return false;
        }
    }

    public void OnCheckCombo()
    {
        //Debug.LogWarning("OnCheckCombo " + _comboOn);
        PlayerBehaviour.instance.animator.ResetTrigger("combo");

        switch (_attackMovementPhase)
        {
            case 1:
                if (_comboOn)
                {
                    //Debug.LogWarning("进入第2段");
                    if (WeaponPowerSystem.instance.power > 0)
                    {
                        isSuperAttack = true;
                        WeaponPowerSystem.instance.ConsumePower_cell(1);
                    }
                    else
                    {
                        isSuperAttack = false;
                    }
                    SetAttackPhase(2);
                    PlayerBehaviour.instance.weaponView.SetState(PlayerWeaponView.State.hide);
                    SyncSwingAnim(2);
                    PlayerBehaviour.instance.animator.SetTrigger("combo");
                    _comboOn = false;
                }
                break;
            case 2:
                if (_comboOn)
                {
                    //Debug.LogWarning("进入第3段");
                    if (WeaponPowerSystem.instance.power > 0)
                    {
                        isSuperAttack = true;
                        WeaponPowerSystem.instance.ConsumePower_cell(1);
                    }
                    else
                    {
                        isSuperAttack = false;
                    }
                    SetAttackPhase(3);
                    PlayerBehaviour.instance.weaponView.SetState(PlayerWeaponView.State.hide);
                    SyncSwingAnim(3);
                    PlayerBehaviour.instance.animator.SetTrigger("combo");
                    _comboOn = false;
                }
                break;
            case 3:
                if (_comboOn)
                {
                    //Debug.LogWarning("进入第1段");
                    if (WeaponPowerSystem.instance.power > 0)
                    {
                        isSuperAttack = true;
                        WeaponPowerSystem.instance.ConsumePower_cell(1);
                    }
                    else
                    {
                        isSuperAttack = false;
                    }
                    SetAttackPhase(1);
                    PlayerBehaviour.instance.weaponView.SetState(PlayerWeaponView.State.hide);
                    SyncSwingAnim(1);
                    PlayerBehaviour.instance.animator.SetTrigger("combo");
                    _comboOn = false;
                }
                break;
        }
    }

    void CheckStopAttack()
    {
        if (_attackMovementPhase == 0)
            return;

        if (_health.isDead || PlayerBehaviour.instance.health.isWounding)
        {
            InterruptAttack();
            return;
        }
        // if (PlayerBehaviour.instance.defend.isInNoMoveState)
        // {
        //     InterruptAttack();
        //     return;
        // }
        var clipInfos = PlayerBehaviour.instance.animator.GetCurrentAnimatorClipInfo(0);
        if (clipInfos.Length < 1)
        {
            return;
        }

        var clipInfo = clipInfos[0];
        var clip = clipInfo.clip;

        switch (_attackMovementPhase)
        {
            case 0:
                if (clip == clip_swing1.clip || clip == clip_swing2.clip || clip == clip_swing3.clip)
                {
                    //Debug.Log(clip);
                    //Debug.LogWarning("不该出现这个情况");
                }
                break;
            case 1:
                if (clip != clip_swing1.clip && clip != clip_swing3.clip)
                {
                    //Debug.Log(clip);
                    //Debug.LogWarning("第1段攻击被打断");
                    InterruptAttack();
                }
                break;
            case 2:
                if (clip != clip_swing1.clip && clip != clip_swing2.clip)
                {
                    //Debug.Log(clip);
                    //Debug.LogWarning("第2段攻击被打断");
                    InterruptAttack();
                }
                break;
            case 3:
                if (clip != clip_swing2.clip && clip != clip_swing3.clip)
                {
                    //Debug.Log(clip);
                    //Debug.LogWarning("第3段攻击被打断");
                    InterruptAttack();
                }
                break;
        }
    }
    void CheckAttack()
    {
        if (_health.isDead)
            return;
        if (PlayerBehaviour.instance.health.isWounding)
            return;
        if (PlayerBehaviour.instance.defend.isDefending)
            return;
        if (PlayerBehaviour.instance.defend.isInDefendEnd)
            return;
        if (PlayerBehaviour.instance.defend.isDeflecting &&
            !PlayerBehaviour.instance.defend.deflectedThisMovement)
            return;
        if (PlayerBehaviour.instance.jump.IsJumping)
            return;
        if (Input.GetKeyDown(KeyCode.J))
        {
            PerformAttack();
        }
    }

    void InterruptAttack()
    {
        Debug.LogWarning("攻击被打断 " + _attackMovementPhase);
        _comboOn = false;
        isSuperAttack = false;
        SetAttackPhase(0);
        PlayerBehaviour.instance.weaponView.SmartSetState();
    }

    void SetAttackPhase(int i)
    {
        //Debug.LogWarning("进入第" + i + "段攻击");
        _attackMovementPhase = i;
    }

    public bool isAttacking { get { return _attackMovementPhase != 0; } }

    void PerformAttack()
    {
        if (PlayerBehaviour.instance.defend.isDeflecting)
        {
            PlayerBehaviour.instance.defend.ExitDefend(false);
        }
        //_move.StopMove();

        //Debug.LogWarning("PerformAttack " + currentAttackSwingPhase);
        switch (_attackMovementPhase)
        {
            case 0:
                //Debug.LogWarning("首次 进入第1段");
                if (WeaponPowerSystem.instance.power > 0)
                {
                    isSuperAttack = true;
                    WeaponPowerSystem.instance.ConsumePower_cell(1);
                }
                else
                {
                    isSuperAttack = false;
                }

                SyncSwingAnim(1);
                PlayerBehaviour.instance.animator.SetTrigger("attack");
                _comboOn = false;
                PlayerBehaviour.instance.weaponView.SetState(PlayerWeaponView.State.hide);
                SetAttackPhase(1);
                break;
            case 1:
                _comboOn = true;
                break;
            case 2:
                _comboOn = true;
                break;
            case 3:
                _comboOn = true;
                break;
        }

    }

    public void CheckDamageCollision(int n)
    {
        //Debug.Log("CheckDamageCollision " + n);
        Collider2D[] cols = null;
        switch (_attackMovementPhase)
        {
            case 1:
                cols = swing1_Cols;
                break;
            case 2:
                cols = swing2_Cols;
                break;
            case 3:
                cols = swing3_Cols;
                break;
        }
        if (cols == null)
        {
            //Debug.LogWarning("no valid Cols! currentAttackSwingPhase " + currentAttackSwingPhase);
            return;
        }
        if (n - 1 >= cols.Length)
        {
            n = cols.Length;
        }

        Collider2D col = cols[n - 1];

        //Vector2 point, Vector2 size, float angle, int layerMask
        var bounds = col.bounds;
        var point = bounds.center;
        var size = bounds.size;
        var targets = Physics2D.OverlapBoxAll(point, size, 0);
        DealDamage(isSuperAttack, targets);
    }

    void DealDamage(bool isSuper, Collider2D[] targets)
    {
        foreach (var t in targets)
        {
            var ene = t.GetComponent<EnemyBehaviour>();
            if (ene != null)
            {
                //ps.Play();
                ene.TakeDamage(GetDamage(isSuper));
            }//敌人

            var meo = t.GetComponent<MeowbodyBehaviour>();
            if (meo != null)
            {
                meo.health.TakeDamage(GetDamage(isSuper));
            }//Meowbody

            var des = t.GetComponent<Destroyable>();
            if (des != null)
            {
                Destroy(des.gameObject);
            }//可摧毁
        }///对所有挂载了以上脚本的物体进行伤害行为
    }///造成伤害
}