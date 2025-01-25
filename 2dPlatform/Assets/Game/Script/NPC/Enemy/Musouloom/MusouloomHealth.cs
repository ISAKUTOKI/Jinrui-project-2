using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusouloomHealth : MonoBehaviour
{
    [HideInInspector]public bool canDie;

    private void Start()
    {
        canDie = false;
    }
    //[SerializeField] int fullHealth = 50;
    public void Die()
    {
        if (canDie)
        {
            Destroy(gameObject);
        }
    }
}
