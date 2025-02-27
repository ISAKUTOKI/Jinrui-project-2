using Assets.Game.Script.HUD_interface.Combat;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class MeowbodyAttackBehaviour : MonoBehaviour
{
    // ����֮��ļ��ʱ��
    public float minAttackInterval = 2f;
    public float maxAttackInterval = 5f;

    // ������ȴʱ��
    public float attack1Cooldown = 5f;
    public float attack2Cooldown = 3f;

    // ��ʱ��
    private float attackTimer;
    private float attack1Timer;
    private float attack2Timer;

    //���ٹ����߼�
    private bool canFastAttack;
    private float fastAttackCD;

    [HideInInspector] public bool isAttacking;
    [HideInInspector] public bool attack1CanAttackNeko;
    [HideInInspector] public bool attack2CanAttackNeko;
    [HideInInspector] public bool fastAttackCanAttackNeko;
    [HideInInspector] public bool diedAttackCanAttackNeko;

    void Start()
    {
        // ��ʼ����ʱ��
        ResetAttackTimer();
        ResetCooldowns();
    }

    void Update()
    {
        // ���¼�ʱ��
        attackTimer -= Time.deltaTime;
        UpdateCooldowns();

        // �����ʱ��������ִ�й���
        if (attackTimer <= 0)
        {
            PerformRandomAttack();
            ResetAttackTimer(); // ���ù��������ʱ��
        }

        //��������������ĳͷ����
        FastAttackCheck();

    }

    // ���ù��������ʱ��
    public void ResetAttackTimer()
    {
        attackTimer = Random.Range(minAttackInterval, maxAttackInterval);
    }
    // �������й�������ȴʱ��
    public void ResetCooldowns()
    {
        attack1Timer = 0;
        attack2Timer = 0;
    }
    // �������й�������ȴʱ��
    private void UpdateCooldowns()
    {
        if (attack1Timer > 0)
            attack1Timer -= Time.deltaTime;
        if (attack2Timer > 0)
            attack2Timer -= Time.deltaTime;
    }

    // ���ѡ��һ�ֹ�����ʽ��ִ��
    private void PerformRandomAttack()
    {
        // ��ȡ���õĹ�����ʽ
        int availableAttacks = GetAvailableAttacks();

        if (availableAttacks == 0)
        {
            return;
        }

        // ���ѡ��һ�ֿ��õĹ�����ʽ
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
    /// ��ȡ��ǰ���õĹ�����ʽ����
    private int GetAvailableAttacks()
    {
        int count = 0;

        if (attack1Timer <= 0) count++;
        if (attack2Timer <= 0) count++;
        return count;
    }

    /// ������λ�ò���������
    private void CheckPlayerPosition(int A_not_A)
    {
        if (MeowbodyBehaviour.instance.Neko.transform == null) return;

        // �����������ڵ��˵�λ��
        Vector3 directionToPlayer = MeowbodyBehaviour.instance.Neko.transform.position - MeowbodyBehaviour.instance.view.transform.position;

        // ���������Ҳ�
        if (directionToPlayer.x * A_not_A > 0)
        {
            FlipRight();
        }
        // �����������
        else if (directionToPlayer.x * A_not_A < 0)
        {
            FlipLeft();
        }
    }

    ///����
    public void Attack1()
    {
        if (attack1Timer > 0) return; // ���������ȴ�У�������
        CheckPlayerPosition(1);
        MeowbodyBehaviour.instance.animator.SetTrigger("attack1");
        //Debug.Log("���ڽ��� Attack 1!");
        attack1Timer = attack1Cooldown; // ������ȴʱ��
        // ������ʵ�ֹ���1�ľ����߼�
    }
    public void Attack2()
    {
        if (attack2Timer > 0) return; // ���������ȴ�У�������
        CheckPlayerPosition(-1);
        MeowbodyBehaviour.instance.animator.SetTrigger("attack2");
        //Debug.Log("���ڽ��� Attack 2!");
        attack2Timer = attack2Cooldown; // ������ȴʱ��
        // ������ʵ�ֹ���2�ľ����߼�
    }
    public void FastAttack()
    {
        ResetAttackTimer();
        ResetCooldowns();
        CheckPlayerPosition(-1);
        MeowbodyBehaviour.instance.animator.SetTrigger("fastAttack");
        MeowbodyBehaviour.instance.health.canBeHurt = false;//���ٹ�����һ��ʱ���ڲ�������
        MeowbodyBehaviour.instance.punishFastAttack.ResetCheckTimer();
        //Debug.Log("���ڽ��� FastAttack!");
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

        if (isAttacking)//���Meowbody�ڹ���
            return;
        if (!MeowbodyBehaviour.instance.punishFastAttack.isChecking)//�����Ҳ���Meowbody�Ŀ��ٹ�����鷶Χ��
            return;
        if (!canFastAttack)//������ܿ��ٹ���
            return;
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (MeowbodyBehaviour.instance.weaponPowerSystem.power < 0.1f)
            {
                FastAttack();
            }
        }
    }
    void FastAttackCheckTimer()//�����˵�һ��ʱ���ڲ��ܿ��ٹ���
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
        //Debug.Log("���ڽ��� DiedAttack!");
    }
    public void AttackTakeDamage(int damage, Collider2D attackCollider, bool isSuperAttack = false)
    {
        PlayerBehaviour.instance.OnHit(attackCollider, isSuperAttack);
    }

    ///ת��
    private void FlipRight()
    {
        //Debug.Log("");
        MeowbodyBehaviour.instance.view.transform.localScale = new Vector3(1, 1, 1); // Ĭ�ϳ����Ҳࣩ
    }
    private void FlipLeft()
    {
        //Debug.Log("");
        MeowbodyBehaviour.instance.view.transform.localScale = new Vector3(-1, 1, 1); // ˮƽ��ת����ࣩ
    }

}
