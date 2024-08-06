using System.Collections;
using UnityEngine;

public class ChatTrigger : MonoBehaviour
{
    public ChatPrototype chat;

    public int maxTriggerTimes = 1;
    int triggeredTimes = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggeredTimes >= maxTriggerTimes)
            return;

        if (other.gameObject.tag == "Player")
        {
            ChatSystem.instance.ShowChat(chat);
            triggeredTimes++;
        }
    }
}