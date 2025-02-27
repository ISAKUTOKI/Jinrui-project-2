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
            Debug.Log("��ҽ���Ģ����ը��Χ");
            playerIsInDamageArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == MusouloomBehaviour.instance.Neko)
        {
            Debug.Log("����˳�Ģ����ը��Χ");
            playerIsInDamageArea = false;
        }
    }
}