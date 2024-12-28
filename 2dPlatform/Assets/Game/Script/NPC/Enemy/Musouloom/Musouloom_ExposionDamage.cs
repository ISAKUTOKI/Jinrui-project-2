using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musouloom_ExposionDamage : MonoBehaviour
{
    public bool playerIsInArea;

    public GameObject Neko;

    private void Start()
    {
        playerIsInArea = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other==Neko)
        {
            playerIsInArea = true;
        }
    }
}
