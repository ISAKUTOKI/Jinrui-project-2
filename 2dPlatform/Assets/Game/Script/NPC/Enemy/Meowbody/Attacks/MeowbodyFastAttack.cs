using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeowbodyFastAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == MeowbodyBehaviour.instance.Neko)
            MeowbodyBehaviour.instance.attack.fastAttackCanAttackNeko = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == MeowbodyBehaviour.instance.Neko)
            MeowbodyBehaviour.instance.attack.fastAttackCanAttackNeko = false;
    }
}
