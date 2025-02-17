using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class ChatText : MonoBehaviour
{
    [SerializeField] List<ChatTextInfo> chat = new List<ChatTextInfo>();
    [SerializeField] bool isPauseChat = false;

    private void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerBehaviour.instance.move.canMove = !isPauseChat;

            ChatBoxSystem.instance.IWantTalk();

            StartCoroutine(PlayChatTextCoroutine());
        }
    }

    private IEnumerator PlayChatTextCoroutine()
    {
        if (chat != null)
        {
            foreach (var item in chat)
            {
                ChatBoxTextMeshBehaviour.instance.SetText(item.text);
                yield return new WaitForSeconds(item.pauseTime);
            }
        }
        else
        {
            ChatBoxTextMeshBehaviour.instance.ClearText();

            PlayerBehaviour.instance.move.canMove = true;
        }
    }
}