using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusouloomExplosionDamageArea : MonoBehaviour
{
    [HideInInspector] public bool playerIsInDamageArea;

    private void Start()
    {
        playerIsInDamageArea = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == MusouloomBehaviour.instance.Neko)
        {
            Debug.Log("Íæ¼Ò½øÈëÄ¢¹½±¬Õ¨·¶Î§");
            playerIsInDamageArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == MusouloomBehaviour.instance.Neko)
        {
            Debug.Log("Íæ¼ÒÍË³öÄ¢¹½±¬Õ¨·¶Î§");
            playerIsInDamageArea = false;
        }
    }
}