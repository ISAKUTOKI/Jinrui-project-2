using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class MusouloomJumpSpot : MonoBehaviour
{
    SpriteRenderer sprite;
    [HideInInspector] public bool canJumpOutFromEarth;
    private void Start()
    {
        canJumpOutFromEarth = true;
        sprite = GetComponent<SpriteRenderer>();

        if (sprite != null && !MusouloomBehaviour.instance.isInTest)
        {
            sprite.enabled = false;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (MusouloomBehaviour.instance.jumpSpot.canJumpOutFromEarth && other.gameObject == MusouloomBehaviour.instance.Neko)
        {
            //Debug.Log("jump");
            MusouloomBehaviour.instance.jump.JumpOutFromEarth();
            MusouloomBehaviour.instance.alarmSpot.canAlarm = false;
        }
    }
}
