using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatMonitor : MonoBehaviour
{
    public bool playerIsInDefendChatArea = false;
    public bool playerIsInDeflectChatIsArea = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (gameObject.name == "DefendChat")
            {
                playerIsInDefendChatArea = true;
            }
            if(gameObject.name== "DeflectChat")
            {
                playerIsInDeflectChatIsArea = true;
            }
        }
    }
}
