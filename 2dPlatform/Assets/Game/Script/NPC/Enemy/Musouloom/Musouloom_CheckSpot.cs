using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musouloom_CheckSpot : MonoBehaviour
{
    public Musouloom_Behaviour musouloom;
    public GameObject Player;
    [HideInInspector] public bool canAlarm;
    [HideInInspector]public bool canJumpOutFromEarth;

    private void Start()
    {
        canAlarm = true;
        canJumpOutFromEarth = false;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (canAlarm && Player)
        {
            //Debug.Log("alarm enter");
            musouloom.idle();
        }
        if (canJumpOutFromEarth && Player)
        {
            Debug.Log("jump");
            //JumpOutFromEarth();
            canAlarm = false;
        }
    }
    void OnTriggerExit2D(Collider2D Player)
    {
        if (Player)
        {
            //Debug.Log("alarm exit");
            musouloom.hide();
        }
    }
}
