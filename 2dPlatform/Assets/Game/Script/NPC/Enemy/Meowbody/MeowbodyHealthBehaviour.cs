using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MeowbodyHealthBehaviour : MonoBehaviour
{   
    // Ѫ������
    public int maxHealth = 100; // ���Ѫ��
    private float currentHealth; // ��ǰѪ��
    void Start()
    {
        // ��ʼ��Ѫ��
        currentHealth = maxHealth;
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.F12))
        //{
        //    Die();
        //}
    }
    /// �ܵ��˺��ķ���
    public void TakeDamage(float damageAmount)
    {
        if (currentHealth <= 0) return; // ����Ѿ����������ٴ����˺�

        MeowbodyBehaviour.instance.attack.ResetAttackTimer();
        MeowbodyBehaviour.instance.attack.ResetCooldowns();
        // ����Ѫ��
        currentHealth -= damageAmount;
        MeowbodyBehaviour.instance.animator.SetTrigger("hurt");
        ////Debug.Log(gameObject.name + " took " + damageAmount + " damage! Current health: " + currentHealth);

        // ����Ƿ�����
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // �����߼�
    public void Die()
    {
        MeowbodyBehaviour.instance.animator.SetTrigger("die");
        Debug.Log(gameObject.name + " has died!");
        // ���ٵ��˶��󣨿��Ը��������滻Ϊ�������������ȣ�
        MeowbodyBehaviour.instance.attack.DiedAttack();
    }

    // ��ȡ��ǰѪ������ѡ��
    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    // ����Ѫ������ѡ��
    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }
    //����
    public void DestroyMeowbody()
    {
        Destroy(gameObject);
    }
}
