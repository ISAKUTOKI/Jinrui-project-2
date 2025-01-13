using Assets.Game.Script.HUD_interface.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeowbodyAttackBehaviour : MonoBehaviour
{
    public WeaponPowerSystem weaponPowerSystem;
    public PlayerHealthBehaviour NekoHealth;
    public Animator animator;
    // 攻击间隔范围
    public float minAttackInterval = 2f;
    public float maxAttackInterval = 5f;

    // 攻击冷却时间
    public float attack1Cooldown = 3f;
    public float attack2Cooldown = 5f;

    // 玩家引用
    public Transform Neko;

    // 计时器
    private float attackTimer;
    private float attack1Timer;
    private float attack2Timer;

    [HideInInspector]public bool canAttackNeko;

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
    }

    // 重置攻击间隔计时器
    private void ResetAttackTimer()
    {
        attackTimer = Random.Range(minAttackInterval, maxAttackInterval);
    }

    // 重置所有攻击的冷却时间
    private void ResetCooldowns()
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
            Debug.Log("No attacks available! Waiting for cooldowns...");
            return;
        }

        // 随机选择一种可用的攻击方式
        int attackType = Random.Range(1, availableAttacks + 1);

        // 检查玩家位置并调整朝向
        CheckPlayerPosition();

        switch (attackType)
        {
            case 1:
                Attack1();
                break;
            case 2:
                Attack2();
                break;
            default:
                Debug.LogWarning("Invalid attack type selected!");
                break;
        }
    }

    // 获取当前可用的攻击方式数量
    private int GetAvailableAttacks()
    {
        int count = 0;

        if (attack1Timer <= 0) count++;
        if (attack2Timer <= 0) count++;
        return count;
    }

    // 检查玩家位置并调整朝向
    private void CheckPlayerPosition()
    {
        if (Neko == null) return;

        // 计算玩家相对于敌人的位置
        Vector3 directionToPlayer = Neko.position - transform.position;

        // 如果玩家在右侧
        if (directionToPlayer.x > 0)
        {
            FlipLeft();
        }
        // 如果玩家在左侧
        else if (directionToPlayer.x < 0)
        {
            FlipRight();
        }
    }

    // 调整朝向为右侧
    private void FlipRight()
    {
        Debug.Log("Flipping Right!");
        transform.localScale = new Vector3(1, 1, 1); // 默认朝向（右侧）
    }

    // 调整朝向为左侧
    private void FlipLeft()
    {
        Debug.Log("Flipping Left!");
        transform.localScale = new Vector3(-1, 1, 1); // 水平翻转（左侧）
    }

    // 攻击方式1
    private void Attack1()
    {
        if (attack1Timer > 0)
            return; // 如果仍在冷却中，则跳过

        animator.SetTrigger("attack1");

        Debug.Log("正在进行 Attack 1!");
        attack1Timer = attack1Cooldown; // 重置冷却时间
        // 在这里实现攻击1的具体逻辑
    }

    // 攻击方式2
    private void Attack2()
    {
        if (attack2Timer > 0) return; // 如果仍在冷却中，则跳过

        animator.SetTrigger("attack2");

        Debug.Log("正在进行 Attack 2!");
        attack2Timer = attack2Cooldown; // 重置冷却时间
        // 在这里实现攻击2的具体逻辑
    }

    private void FastAttack()
    {
        Debug.Log("正在进行 FastAttack!");
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (weaponPowerSystem.power < 0.1f)
            {
                animator.SetTrigger("fastAttack");
            }
        }
    }

    [HideInInspector]
    public void AttackTakeDamage(int attackDamage)
    {
        if (canAttackNeko)
        {
            NekoHealth.TakeDamage(attackDamage);
        }
    }
}
