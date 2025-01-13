using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meowbody_AnimeEvents : MonoBehaviour
{
    public MeowbodyHealthBehaviour health;
    public MeowbodyAttackBehaviour attack;
    public void Attack1Hit()
    {
        attack.AttackTakeDamage(1);
    }

    public void Attack2Hit()
    {
        attack.AttackTakeDamage(1);
    }

    public void FastAttack()
    {
        attack.AttackTakeDamage(2);
    }

    public void HurtEnd()
    {
        health.isHurt = false;
    }
}
