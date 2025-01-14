using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meowbody_AnimeEvents : MonoBehaviour
{
    public MeowbodyGetComponent component;
    public void Attack1Hit()
    {
        if (component.attackSystem.attack1CanAttackNeko)
        {
            component.attackSystem.AttackTakeDamage(1);
        }
    }

    public void Attack2Hit()
    {
        if (component.attackSystem.attack2CanAttackNeko)
        {
            component.attackSystem.AttackTakeDamage(1);
        }
    }

    public void FastAttack()
    {
        if (component.attackSystem.fastAttackCanAttackNeko)
        {
            component.attackSystem.AttackTakeDamage(2);
        }
    }

    public void DiedAttack()
    {
        if (component.attackSystem.diedAttackCanAttackNeko)
        {
            component.attackSystem.AttackTakeDamage(1);
        }
    }

    public void AttackBegin()
    {
        component.attackSystem.isAttacking = true;
    }
    public void AttackEnd()
    {
        component.attackSystem.isAttacking = false;
    }
    public void TimeToDie()
    {
        component.attackSystem.DestroyMeowbody();
    }
}
