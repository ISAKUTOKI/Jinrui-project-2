using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class ChatText : MonoBehaviour
{
    [SerializeField] List<ChatTextInfo> chat = new List<ChatTextInfo>();
    [SerializeField] bool isPauseChat = false;
    [SerializeField] bool isExitEndChat = false;
    [SerializeField] bool canChatAgain = false;
    bool isChatted = false;
    Coroutine currentCoroutine;

    private void Start()
    {
        chat.Add(new ChatTextInfo("", 0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isChatted)
            return;
        if (collision.CompareTag("Player"))
        {
            if (isPauseChat)
            {
                PlayerBehaviour.instance.animator.SetBool("walk", false);
                PlayerBehaviour.instance.movePosition.StopXMovement();
                PlayerBehaviour.instance.move.canMove = false;
            }

            ChatBoxSystem.instance.IWantTalk();

            currentCoroutine = StartCoroutine(PlayChatTextCoroutine());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isExitEndChat)
            {
                ChatBoxSystem.instance.IWantStop();
                PlayerBehaviour.instance.move.canMove = true;
                if (currentCoroutine != null)
                {
                    StopCoroutine(currentCoroutine);
                }
                ChatBoxTextMeshBehaviour.instance.SetText("");
            }
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

        //Debug.Log("Chat text end");
        ChatBoxSystem.instance.IWantStop();
        PlayerBehaviour.instance.move.canMove = true;

        if (canChatAgain)
        {
            isChatted = false;
        }
        else
        {
            isChatted = true;
        }
    }
}