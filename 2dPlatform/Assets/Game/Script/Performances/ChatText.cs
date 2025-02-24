using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using static UnityEditor.Progress;

public class ChatText : MonoBehaviour
{
    [SerializeField] List<ChatTextInfo> chat = new List<ChatTextInfo>();
    [SerializeField] bool isPauseChat = false;
    [SerializeField] bool isExitEndChat = false;
    [SerializeField] bool canChatAgain = false;
    [SerializeField] bool isSafeChat = false;
    bool isChatted = false;
    Coroutine currentCoroutine;

    private void Start()
    {
        //chat.Add(new ChatTextInfo("", 0, ChatTextInfo.ChatBoxAction.Stop));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
                EndChat();
            }
        }
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
            if (isSafeChat)
            {
                PlayerBehaviour.instance.health.canBeWounded = false;
            }

            //ChatBoxSystem.instance.IWantTalk();

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
                PlayerBehaviour.instance.health.canBeWounded = true;
            }
        }
    }

    private IEnumerator PlayChatTextCoroutine()
    {
        if (canChatAgain)
        {
            isChatted = false;
        }
        else
        {
            isChatted = true;
        }

        if (chat != null)
        {
            foreach (var item in chat)
            {
                item.InvokeChatBoxAction();
                ChatBoxTextMeshBehaviour.instance.SetText(item.text);
                yield return new WaitForSeconds(item.duration);
                //Debug.Log(isPauseChat);
            }
        }

        EndChat();
    }

    private void EndChat()
    {
        if (chat.Count > 0)
        {
            var lastChatAction = chat[chat.Count - 1].chatBoxAction;
            if (lastChatAction != ChatTextInfo.ChatBoxAction.Stop && lastChatAction != ChatTextInfo.ChatBoxAction.FastStop)
            {
                ChatBoxSystem.instance.IWantStop();
            }
            ChatBoxTextMeshBehaviour.instance.SetText("");
        }

        PlayerBehaviour.instance.move.canMove = true;
        PlayerBehaviour.instance.health.canBeWounded = true;
    }
}
