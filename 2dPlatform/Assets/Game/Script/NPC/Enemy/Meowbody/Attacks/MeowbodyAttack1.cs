using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeowbodyAttack1 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == MeowbodyBehaviour.instance.Neko)
            MeowbodyBehaviour.instance.attack.attack1CanAttackNeko = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == MeowbodyBehaviour.instance.Neko)
            MeowbodyBehaviour.instance.attack.attack1CanAttackNeko = false;
    }
}
