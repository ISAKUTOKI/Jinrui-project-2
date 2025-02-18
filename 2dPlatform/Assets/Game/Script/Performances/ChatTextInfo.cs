using System;
using UnityEngine;

[Serializable]
public class ChatTextInfo
{
    public string text;
    public float pauseTime;
    public ChatBoxAction chatBoxAction;

    public ChatTextInfo(string t, float p, ChatBoxAction action)
    {
        text = t;
        pauseTime = p;
        chatBoxAction = action;
    }

    public enum ChatBoxAction
    {
        None,
        Talk,
        Show,
        Shock,
        Hurt,
        Stop
    }

    public void InvokeChatBoxAction()
    {
        switch (chatBoxAction)
        {
            case ChatBoxAction.Talk:
                ChatBoxSystem.instance.IWantTalk();
                break;
            case ChatBoxAction.Show:
                ChatBoxSystem.instance.IWantShow();
                break;
            case ChatBoxAction.Shock:
                ChatBoxSystem.instance.IWantShock();
                break;
            case ChatBoxAction.Hurt:
                ChatBoxSystem.instance.IWantHurt();
                break;
            case ChatBoxAction.None:
                break;
            case ChatBoxAction.Stop:
                ChatBoxSystem.instance.IWantStop();
                break;
            default:
                Debug.LogWarning("Unknown ChatBoxAction: " + chatBoxAction);
                break;
        }
    }
}