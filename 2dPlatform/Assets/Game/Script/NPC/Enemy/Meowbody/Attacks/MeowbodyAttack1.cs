using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeowbodyAttack1 : MonoBehaviour
{
    public GameObject Neko;
    public MeowbodyAttackBehaviour attackBehaviour;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == Neko)
            attackBehaviour.canAttackNeko = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == Neko)
            attackBehaviour.canAttackNeko = false;
    }
}
