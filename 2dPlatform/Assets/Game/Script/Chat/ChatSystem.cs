using System.Collections;
using UnityEngine;


public class ChatSystem : MonoBehaviour
{
    public static ChatSystem instance { get; private set; }

    private ChatPrototype _chat;

    public string flag;

    private void Awake()
    {
        instance = this;
    }


    public ChatPrototype testStartingChat;

    private void Start()
    {
        ChatPanelBehaviour.instance.Hide();

        StartCoroutine(TestChat());
    }

    IEnumerator TestChat()
    {
        yield return new WaitForSeconds(1);
        if (testStartingChat != null)
            ShowChat(testStartingChat);
    }

    public void ShowChat(ChatPrototype chat)
    {
        if (chat == null)
        {
            Debug.LogWarning("show empty chat!");
            return;
        }

        PauseSystem.instance.Pause();
        _chat = chat;
        ChatPanelBehaviour.instance.Show(_chat);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChatPanelBehaviour.instance.UserTapped();
        }
    }

    public void EndChat()
    {
        PauseSystem.instance.Resume();

        ChatPanelBehaviour.instance.Hide();

        switch (_chat.chatSpecialAction)
        {
            case ChatPrototype.ChatSpecialAction.None:
                break;
        }
        if (flag == "kick girl")
        {
           
        }
        _chat = null;
    }

    public bool IsChating()
    {
        return _chat != null;
    }

    public void OnChatEnd()
    {
        if (_chat == null)
            return;
        flag = _chat.flag;

        if (_chat.next == null)
        {
            EndChat();
            return;
        }

        ShowChat(_chat.next);
    }
}