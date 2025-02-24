using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MeowbodyHealthBehaviour : MonoBehaviour
{   
    // 血量属性
    public int maxHealth = 100; // 最大血量
    private float currentHealth; // 当前血量
    void Start()
    {
        // 初始化血量
        currentHealth = maxHealth;
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.F12))
        //{
        //    Die();
        //}
    }
    /// 受到伤害的方法
    public void TakeDamage(float damageAmount)
    {
        if (currentHealth <= 0) return; // 如果已经死亡，则不再处理伤害

        MeowbodyBehaviour.instance.attack.ResetAttackTimer();
        MeowbodyBehaviour.instance.attack.ResetCooldowns();
        // 减少血量
        currentHealth -= damageAmount;
        MeowbodyBehaviour.instance.animator.SetTrigger("hurt");
        ////Debug.Log(gameObject.name + " took " + damageAmount + " damage! Current health: " + currentHealth);

        // 检查是否死亡
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // 死亡逻辑
    public void Die()
    {
        MeowbodyBehaviour.instance.animator.SetTrigger("die");
        Debug.Log(gameObject.name + " has died!");
        // 销毁敌人对象（可以根据需求替换为播放死亡动画等）
        MeowbodyBehaviour.instance.attack.DiedAttack();
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
    //死亡
    public void DestroyMeowbody()
    {
        Destroy(gameObject);
    }
}
