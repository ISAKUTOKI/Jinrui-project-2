using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musouloom_AlarmSpot : MonoBehaviour
{
    public GameObject Neko;
    public Musouloom_Behaviour musouloom;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (musouloom.canAlarm && other.gameObject == Neko)
        {
            Debug.Log("alarm enter");
            musouloom.idle();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (musouloom.canAlarm && other.gameObject == Neko)
        {
            Debug.Log("alarm exit");
            musouloom.hide();
        }
    }
}
