using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musouloom_ExplosionCheck : MonoBehaviour
{
    public GameObject Neko;
    public Musouloom_Behaviour musouloom;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == Neko)
            musouloom.Explosion();
    }
}
