using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EventTrigger : MonoBehaviour
{
    public UnityEvent evts;
    public bool onceOnly;
    int times;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (onceOnly && times > 0)
                return;

            times++;
            evts?.Invoke();
        }
    }
}