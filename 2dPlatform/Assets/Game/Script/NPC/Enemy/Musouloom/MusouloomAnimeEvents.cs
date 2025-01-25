using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusouloomAnimeEvents : MonoBehaviour
{
    // Start is called before the first frame update
    public void TimeToDie()
    {
        MusouloomBehaviour.instance.health.Die();
    }

    public void SearchBegin()
    {
        MusouloomBehaviour.instance.move.canSearch = true;
    }

    public void SearchEnd()
    {
        MusouloomBehaviour.instance.move.canSearch = false;
    }

    public void ExplosionDamage()
    {
        MusouloomBehaviour.instance.exposion.ExplosionHurtPlayer();
    }
}
