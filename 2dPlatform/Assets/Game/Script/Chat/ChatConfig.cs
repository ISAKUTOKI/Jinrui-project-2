using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class ChatConfig : ScriptableObject
{
    public List<ChatCharacterPrototype> chatCharacters;

    public ChatCharacterPrototype GetChatCharacterPrototype(string id)
    {
        foreach (ChatCharacterPrototype chatCharacter in chatCharacters)
        {
            if (chatCharacter.id == id)
                return chatCharacter;
        }
        return null;
    }

}