using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MeowbodyHealthBehaviour : MonoBehaviour
{
    public int maxHealth = 100;
    private float currentHealth;
    [HideInInspector] public bool isHurting = false;
    [HideInInspector] public int hurtCount = 0;

    float hurtCD = 0.7f;
    [HideInInspector] public bool canBeHurt = true;
    void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.F12))
        //{
        //    Die();
        //}

        if (!canBeHurt)
        {
            //Debug.Log("��������");
            hurtCD -= Time.deltaTime;
            if (hurtCD <= 0)
            {
                //Debug.Log("��������");
                canBeHurt = true;
                hurtCD = 0.7f;
            }
        }
    }
    /// �ܵ��˺��ķ���
    public void TakeDamage(float damageAmount)
    {
        hurtCount++;
        if (currentHealth <= 0) return;

        isHurting = true;
        MeowbodyBehaviour.instance.attack.ResetAttackTimer();
        MeowbodyBehaviour.instance.attack.ResetCooldowns();
        // ����Ѫ��
        currentHealth -= damageAmount;
        MeowbodyBehaviour.instance.animator.SetTrigger("hurt");
        if (currentHealth <= 0)
        {
            Die();
        }
        isHurting = false;
    }

    public void Die()
    {
        MeowbodyBehaviour.instance.animator.SetTrigger("die");
        Debug.Log(gameObject.name + " has died!");
        MeowbodyBehaviour.instance.attack.DiedAttack();
    }

    public void DestroyMeowbody()
    {
        Destroy(gameObject);
    }
}
