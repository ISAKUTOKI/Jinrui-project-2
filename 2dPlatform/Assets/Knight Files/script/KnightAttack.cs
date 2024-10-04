using com;
using System.Collections;
using UnityEngine;



public class KnightAttack : MonoBehaviour
{
    public Animator animator;
    public DeflectArea deflectArea;
    public KnightGroundCheck kgc;
    private Rigidbody2D rb;
    float attackInterval = 0.42f;
    float lastAttackTime;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("j") && CanAttack())
        {
            AttackStart();
        }
    }

    void AttackStart()
    {
        animator.SetTrigger("attack");
        lastAttackTime = Time.time;
    }

    public void AttackHit()
    {
        Deflect();
    }

    void AttackEnd()
    {

    }

    bool CanAttack()
    {
        //在地面上
        //不在被打硬直
        //攻击CD（不在上一次攻击中）
        if (!kgc.isOnGround)
            return false;
        if (IsInAttackCD())
            return false;
        if (IsHurting())
            return false;

        return true;
    }



    bool IsOnGround()
    {
        return true;
    }

    bool IsHurting()
    {
        if (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Hurt Animation")
        {
            return true;
        }
        return false;
    }

    bool IsInAttackCD()
    {
        if (Time.time - lastAttackTime < attackInterval)
        {
            return true;
        }
        return false;
    }

    public bool IsAttacking()
    {
        if (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Attack Animation")
        {
            return true;
        }
        return false;
    }

    void Deflect()
    {
        foreach (var p in deflectArea.targets)
        {
            if (p == null)
            {
                continue;
            }
            p.Reverse();
        }
    }


}
