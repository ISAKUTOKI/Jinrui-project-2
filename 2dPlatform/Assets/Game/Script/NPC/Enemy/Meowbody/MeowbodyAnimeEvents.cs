using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeowbodyAnimeEvents : MonoBehaviour
{

    public void Attack1Hit()
    {
        if (MeowbodyBehaviour.instance.attack.attack1CanAttackNeko)
        {
            MeowbodyBehaviour.instance.attack.AttackTakeDamage(1);
        }
    }

    public void Attack2Hit()
    {
        if (MeowbodyBehaviour.instance.attack.attack2CanAttackNeko)
        {
            MeowbodyBehaviour.instance.attack.AttackTakeDamage(1);
        }
    }

    public void FastAttack()
    {
        if (MeowbodyBehaviour.instance.attack.fastAttackCanAttackNeko)
        {
            MeowbodyBehaviour.instance.attack.AttackTakeDamage(2);
        }
    }

    public void DiedAttack()
    {
        if (MeowbodyBehaviour.instance.attack.diedAttackCanAttackNeko)
        {
            MeowbodyBehaviour.instance.attack.AttackTakeDamage(1);
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
