using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusouloomExposion : MonoBehaviour
{
    public void Explosion()
    {
        MusouloomBehaviour.instance.animator.SetTrigger("explosion");
        StartCoroutine(MusouloomBehaviour.instance.jump.ExplosionJump(0.6f, 0.1f));
        //Debug.Log("OK");
    }

   
    public void ExplosionHurtPlayer()
    {
        if (MusouloomBehaviour.instance.explosionDamageArea.playerIsInDamageArea)
        {
            PlayerBehaviour.instance.health.TakeDamage(2);
        }
    }
}
