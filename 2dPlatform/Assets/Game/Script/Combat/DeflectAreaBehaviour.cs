using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeflectAreaBehaviour : MonoBehaviour
{
    private List<EnemyBehaviour> enes;

    private void OnEnable()
    {
        enes = new List<EnemyBehaviour>();
    }

    private void OnDisable()
    {
        enes = new List<EnemyBehaviour>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var ene = collision.GetComponent<EnemyBehaviour>();
        if (enes.Contains(ene))
        {
            return;
        }

        enes.Add(ene);
    }

    public bool CheckDelfect(EnemyBehaviour e)
    {
        return enes.Contains(e);
    }
}
