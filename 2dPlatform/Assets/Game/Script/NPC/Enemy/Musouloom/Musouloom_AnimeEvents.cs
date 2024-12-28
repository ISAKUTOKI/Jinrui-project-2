using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musouloom_AnimeEvents : MonoBehaviour
{
    public Musouloom_Behaviour musouloom;
    // Start is called before the first frame update
    public void TimeToDie()
    {
        musouloom.Die();
    }

    public void SearchBegin()
    {
        musouloom.canSearch = true;
    }

    public void SearchEnd()
    {
        musouloom.canSearch = false;
    }

    public void ExplosionDamage()
    {
        musouloom.ExplosionHurtPlayer();
    }
}
