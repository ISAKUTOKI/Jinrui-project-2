using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MeowbodyHealthBehaviour : MonoBehaviour
{
    public Animator animator;

    // 血量属性
    public float maxHealth = 100f; // 最大血量
    private float currentHealth; // 当前血量

    [HideInInspector]public bool isHurt;


    void Start()
    {
        isHurt = false;
        // 初始化血量
        currentHealth = maxHealth;
    }

    // 受到伤害的方法
    public void TakeDamage(float damageAmount)
    {
        if (currentHealth <= 0) return; // 如果已经死亡，则不再处理伤害

        isHurt=true;
        // 减少血量
        currentHealth -= damageAmount;
        animator.SetTrigger("hurt");
        Debug.Log(gameObject.name + " took " + damageAmount + " damage! Current health: " + currentHealth);

        // 检查是否死亡
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // 死亡逻辑
    private void Die()
    {
        Debug.Log(gameObject.name + " has died!");

        // 销毁敌人对象（可以根据需求替换为播放死亡动画等）
        animator.SetTrigger("die");
    }

    // 获取当前血量（可选）
    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    // 重置血量（可选）
    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }
}
