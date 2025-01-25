using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusouloomAlarmSpot : MonoBehaviour
{
    SpriteRenderer sprite;
    [HideInInspector] public bool canAlarm;
    private void Start()
    {
        canAlarm = true;
        sprite = GetComponent<SpriteRenderer>();

        if (sprite != null && !MusouloomBehaviour.instance.isInTest)
        {
            sprite.enabled = false;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (canAlarm && other.gameObject == MusouloomBehaviour.instance.Neko)
        {
            MusouloomBehaviour.instance.animator.SetBool("idle", true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (canAlarm && other.gameObject == MusouloomBehaviour.instance.Neko)
        {
            MusouloomBehaviour.instance.animator.SetBool("idle", false);
        }
    }
}
