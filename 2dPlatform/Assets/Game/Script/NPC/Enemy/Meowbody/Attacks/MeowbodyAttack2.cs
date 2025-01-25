using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeowbodyAttack2 : MonoBehaviour
{   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == MeowbodyBehaviour.instance.Neko)
            MeowbodyBehaviour.instance.attack.attack2CanAttackNeko = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == MeowbodyBehaviour.instance.Neko)
            MeowbodyBehaviour.instance.attack.attack2CanAttackNeko = false;
    }
}
