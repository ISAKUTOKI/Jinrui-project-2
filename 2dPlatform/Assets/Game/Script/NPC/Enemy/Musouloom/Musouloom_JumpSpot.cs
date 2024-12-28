using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musouloom_JumpSpot : MonoBehaviour
{
    public GameObject Neko;
    public Musouloom_Behaviour musouloom;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (musouloom.canJumpOutFromEarth && other.gameObject==Neko)
        {
            Debug.Log("jump");
            musouloom.JumpOutFromEarth();
            musouloom.canAlarm = false;
        }
    }
}
