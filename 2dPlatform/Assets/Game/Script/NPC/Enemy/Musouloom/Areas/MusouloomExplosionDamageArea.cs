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
        //Debug.Log("有东西进入区域 " + other.name);
        if (other.gameObject == MusouloomBehaviour.instance.Neko)
        {
            Debug.Log("玩家进入蘑菇爆炸范围");
            playerIsInDamageArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == MusouloomBehaviour.instance.Neko)
        {
            Debug.Log("玩家退出蘑菇爆炸范围");
            playerIsInDamageArea = false;
        }
    }
}
