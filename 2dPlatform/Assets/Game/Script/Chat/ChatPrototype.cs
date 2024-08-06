using UnityEngine;

[CreateAssetMenu]
public class ChatPrototype : ScriptableObject
{
    public string characterId;
    [Multiline]
    public string content;
    public string soundName;

    public ChatSpecialAction chatSpecialAction;

    public ChatPrototype next;
    public enum ChatSpecialAction
    {
        None,

    }

    public string flag;
}