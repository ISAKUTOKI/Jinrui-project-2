using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeowbodyAttack2 : MonoBehaviour
{
    public MeowbodyGetComponent component;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == component.Neko)
            component.attackSystem.attack2CanAttackNeko = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == component.Neko)
            component.attackSystem.attack2CanAttackNeko = false;
    }
}
