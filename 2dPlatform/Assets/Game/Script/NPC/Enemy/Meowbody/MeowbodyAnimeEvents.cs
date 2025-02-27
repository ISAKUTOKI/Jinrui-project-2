using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeowbodyAnimeEvents : MonoBehaviour
{

    public void Attack1Hit()
    {
        if (MeowbodyBehaviour.instance.attack.attack1CanAttackNeko)
        {
            var attackCollider = MeowbodyBehaviour.instance.attack1.GetComponent<Collider2D>();
            MeowbodyBehaviour.instance.attack.AttackTakeDamage(1, attackCollider);
        }
    }

    public void Attack2Hit()
    {
        if (MeowbodyBehaviour.instance.attack.attack2CanAttackNeko)
        {
            var attackCollider = MeowbodyBehaviour.instance.attack2.GetComponent<Collider2D>();
            MeowbodyBehaviour.instance.attack.AttackTakeDamage(1, attackCollider);
        }
    }

    public void FastAttack()
    {
        if (MeowbodyBehaviour.instance.attack.fastAttackCanAttackNeko)
        {
            var attackCollider = MeowbodyBehaviour.instance.fastAttack.GetComponent<Collider2D>();
            MeowbodyBehaviour.instance.attack.AttackTakeDamage(2, attackCollider, true);
        }
    }

    public void DiedAttack()
    {
        if (MeowbodyBehaviour.instance.attack.diedAttackCanAttackNeko)
        {
            var attackCollider = MeowbodyBehaviour.instance.diedAttack.GetComponent<Collider2D>();
            MeowbodyBehaviour.instance.attack.AttackTakeDamage(1, attackCollider);
        }
    }


    public void AttackBegin()
    {
        MeowbodyBehaviour.instance.attack.isAttacking = true;
    }
    public void AttackEnd()
    {
        MeowbodyBehaviour.instance.attack.isAttacking = false;
    }

    public void TimeToDie()
    {
        MeowbodyBehaviour.instance.health.DestroyMeowbody();
    }
}
