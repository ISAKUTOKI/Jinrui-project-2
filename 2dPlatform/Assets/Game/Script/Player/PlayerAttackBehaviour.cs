using System.Collections;
using UnityEngine;

public class PlayerAttackBehaviour : MonoBehaviour
{
    private PlayerJump _jump;
    private Rigidbody2D rb;

    //public Transform damageOrigin;
    //public float damageRadius = 1.5f;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public int GetDamage(bool isSuper)
    {
        switch (currentAttackSwingPhase)
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

    private PlayerHealthBehaviour _health;
    //public ParticleSystem ps;

    public AnimationClipData clip_swing1;
    public AnimationClipData clip_swing2;
    public AnimationClipData clip_swing3;

    public Collider2D[] swing1_Cols;
    public Collider2D[] swing2_Cols;
    public Collider2D[] swing3_Cols;

    public int damage_swing1;
    public int damage_swing1_Super;
    public int damage_swing2;
    public int damage_swing2_Super;
    public int damage_swing3;
    public int damage_swing3_Super;
    public int currentAttackSwingPhase;//0 没有攻击 1~3 第1~3次斩击 
    public bool isSuperAttack;//弹反攻击
    //public PlayerActionPerformDependency dependency;
    private bool _comboOn;

    private void Awake()
    {
        _jump = GetComponent<PlayerJump>();
        _health = GetComponent<PlayerHealthBehaviour>();
        currentAttackSwingPhase = 0;
        isSuperAttack = false;
    }

    private void Update()
    {
        CheckStopAttack();
        CheckAttack();
    }

    public void OnCheckCombo()
    {
        //Debug.LogWarning("OnCheckCombo " + _comboOn);
        PlayerBehaviour.instance.animator.ResetTrigger("combo");

        switch (currentAttackSwingPhase)
        {
            case 1:
                if (_comboOn)
                {
                    //Debug.LogWarning("进入第2段");
                    SetAttackPhase(2);
                    PlayerBehaviour.instance.weaponView.SetState(PlayerWeaponView.State.hide);
                    PlayerBehaviour.instance.animator.SetTrigger("combo");
                    _comboOn = false;
                }
                break;
            case 2:
                if (_comboOn)
                {
                    //Debug.LogWarning("进入第3段");
                    SetAttackPhase(3);
                    PlayerBehaviour.instance.weaponView.SetState(PlayerWeaponView.State.hide);
                    PlayerBehaviour.instance.animator.SetTrigger("combo");
                    _comboOn = false;
                }
                break;
            case 3:
                if (_comboOn)
                {
                    //Debug.LogWarning("进入第1段");
                    SetAttackPhase(1);
                    PlayerBehaviour.instance.weaponView.SetState(PlayerWeaponView.State.hide);
                    PlayerBehaviour.instance.animator.SetTrigger("combo");
                    _comboOn = false;
                }
                break;
        }
    }

    void CheckStopAttack()
    {
        if (currentAttackSwingPhase == 0)
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
        var clipInfo = PlayerBehaviour.instance.animator.GetCurrentAnimatorClipInfo(0)[0];
        var clip = clipInfo.clip;
        switch (currentAttackSwingPhase)
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
        if (PlayerBehaviour.instance.defend.isInNoMoveState)
            return;
        if (Input.GetKeyDown(KeyCode.J))
        {
            TryAttack();
        }
    }

    void InterruptAttack()
    {
        //Debug.LogWarning("攻击被打断 " + currentAttackSwingPhase);
        _comboOn = false;
        isSuperAttack = false;
        currentAttackSwingPhase = 0;
        PlayerBehaviour.instance.weaponView.SetState(PlayerWeaponView.State.idle);
    }

    void SetAttackPhase(int i)
    {
        //Debug.LogWarning("进入第" + i + "段攻击");
        currentAttackSwingPhase = i;
    }

    public bool isAttacking { get { return currentAttackSwingPhase != 0; } }

    void TryAttack()
    {
        PerformAttack();
    }

    void PerformAttack()
    {
        switch (currentAttackSwingPhase)
        {
            case 0:
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
        Debug.Log("CheckDamageCollision " + n);
        Collider2D[] cols = null;
        switch (currentAttackSwingPhase)
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
            Debug.LogWarning("no valid Cols! currentAttackSwingPhase " + currentAttackSwingPhase);
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
            }

            var des = t.GetComponent<Destroyable>();
            if (des != null)
            {
                Destroy(des.gameObject);
            }
        }
    }
}