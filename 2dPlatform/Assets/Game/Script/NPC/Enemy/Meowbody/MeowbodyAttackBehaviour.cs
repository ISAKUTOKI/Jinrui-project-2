using Assets.Game.Script.HUD_interface.Combat;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class MeowbodyAttackBehaviour : MonoBehaviour
{
    // 攻击之间的间隔时间
    public float minAttackInterval = 2f;
    public float maxAttackInterval = 5f;

    // 攻击冷却时间
    public float attack1Cooldown = 5f;
    public float attack2Cooldown = 3f;

    // 计时器
    private float attackTimer;
    private float attack1Timer;
    private float attack2Timer;

    //快速攻击逻辑
    private bool canFastAttack;
    private float fastAttackCD;

    [HideInInspector] public bool isAttacking;
    [HideInInspector] public bool attack1CanAttackNeko;
    [HideInInspector] public bool attack2CanAttackNeko;
    [HideInInspector] public bool fastAttackCanAttackNeko;
    [HideInInspector] public bool diedAttackCanAttackNeko;

    void Start()
    {
        // 初始化计时器
        ResetAttackTimer();
        ResetCooldowns();
    }

    void Update()
    {
        // 更新计时器
        attackTimer -= Time.deltaTime;
        UpdateCooldowns();

        // 如果计时器结束，执行攻击
        if (attackTimer <= 0)
        {
            PerformRandomAttack();
            ResetAttackTimer(); // 重置攻击间隔计时器
        }

        //玩家无能量攻击的惩罚检测
        FastAttackCheck();

    }

    // 重置攻击间隔计时器
    public void ResetAttackTimer()
    {
        attackTimer = Random.Range(minAttackInterval, maxAttackInterval);
    }
    // 重置所有攻击的冷却时间
    public void ResetCooldowns()
    {
        attack1Timer = 0;
        attack2Timer = 0;
    }
    // 更新所有攻击的冷却时间
    private void UpdateCooldowns()
    {
        if (attack1Timer > 0)
            attack1Timer -= Time.deltaTime;
        if (attack2Timer > 0)
            attack2Timer -= Time.deltaTime;
    }

    // 随机选择一种攻击方式并执行
    private void PerformRandomAttack()
    {
        // 获取可用的攻击方式
        int availableAttacks = GetAvailableAttacks();

        if (availableAttacks == 0)
        {
            return;
        }

        // 随机选择一种可用的攻击方式
        int attackType = Random.Range(1, availableAttacks + 1);

        switch (attackType)
        {
            case 1:
                Attack1();
                break;
            case 2:
                Attack2();
                break;
            default:
                //Debug.LogWarning("Invalid attack type selected!");
                break;
        }
    }
    /// 获取当前可用的攻击方式数量
    private int GetAvailableAttacks()
    {
        int count = 0;

        if (attack1Timer <= 0) count++;
        if (attack2Timer <= 0) count++;
        return count;
    }

    /// 检查玩家位置并调整朝向
    private void CheckPlayerPosition(int A_not_A)
    {
        if (MeowbodyBehaviour.instance.Neko.transform == null) return;

        // 计算玩家相对于敌人的位置
        Vector3 directionToPlayer = MeowbodyBehaviour.instance.Neko.transform.position - MeowbodyBehaviour.instance.view.transform.position;

        // 如果玩家在右侧
        if (directionToPlayer.x * A_not_A > 0)
        {
            FlipRight();
        }
        // 如果玩家在左侧
        else if (directionToPlayer.x * A_not_A < 0)
        {
            FlipLeft();
        }
    }

    ///攻击
    public void Attack1()
    {
        if (attack1Timer > 0) return; // 如果仍在冷却中，则跳过
        CheckPlayerPosition(1);
        MeowbodyBehaviour.instance.animator.SetTrigger("attack1");
        //Debug.Log("正在进行 Attack 1!");
        attack1Timer = attack1Cooldown; // 重置冷却时间
        // 在这里实现攻击1的具体逻辑
    }
    public void Attack2()
    {
        if (attack2Timer > 0) return; // 如果仍在冷却中，则跳过
        CheckPlayerPosition(-1);
        MeowbodyBehaviour.instance.animator.SetTrigger("attack2");
        //Debug.Log("正在进行 Attack 2!");
        attack2Timer = attack2Cooldown; // 重置冷却时间
        // 在这里实现攻击2的具体逻辑
    }
    public void FastAttack()
    {
        ResetAttackTimer();
        ResetCooldowns();
        CheckPlayerPosition(-1);
        MeowbodyBehaviour.instance.animator.SetTrigger("fastAttack");
        MeowbodyBehaviour.instance.health.canBeHurt = false;//快速攻击后一段时间内不会受伤
        MeowbodyBehaviour.instance.punishFastAttack.ResetCheckTimer();
        //Debug.Log("正在进行 FastAttack!");
        attack1Timer = 0;
        attack2Timer = 0;
    }
    void FastAttackCheck()
    {
        if (MeowbodyBehaviour.instance.health.hurtCount == 5)
        {
            MeowbodyBehaviour.instance.health.hurtCount = 0;
            FastAttack();
        }

        FastAttackCheckTimer();

        if (isAttacking)//如果Meowbody在攻击
            return;
        if (!MeowbodyBehaviour.instance.punishFastAttack.isChecking)//如果玩家不在Meowbody的快速攻击检查范围内
            return;
        if (!canFastAttack)//如果不能快速攻击
            return;
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (MeowbodyBehaviour.instance.weaponPowerSystem.power < 0.1f)
            {
                FastAttack();
            }
        }
    }
    void FastAttackCheckTimer()//刚受伤的一段时间内不能快速攻击
    {
        if (MeowbodyBehaviour.instance.health.isHurting)
        {
            fastAttackCD = 0.5f;
        }
        fastAttackCD -= Time.deltaTime;
        if (fastAttackCD <= 0)
        {
            canFastAttack = true;
        }
        else
        {
            canFastAttack = false;
        }
    }
    public void DiedAttack()
    {
        CheckPlayerPosition(-1);
        //Debug.Log("正在进行 DiedAttack!");
    }
    public void AttackTakeDamage(int damage, Collider2D attackCollider, bool isSuperAttack = false)
    {
        PlayerBehaviour.instance.OnHit(attackCollider, isSuperAttack);
    }

    ///转向
    private void FlipRight()
    {
        //Debug.Log("");
        MeowbodyBehaviour.instance.view.transform.localScale = new Vector3(1, 1, 1); // 默认朝向（右侧）
    }
    private void FlipLeft()
    {
        //Debug.Log("");
        MeowbodyBehaviour.instance.view.transform.localScale = new Vector3(-1, 1, 1); // 水平翻转（左侧）
    }

}
