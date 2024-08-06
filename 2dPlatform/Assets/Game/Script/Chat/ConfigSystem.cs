using UnityEngine;

public class ConfigSystem : MonoBehaviour
{
    public static ConfigSystem instance { get; private set; }

    public ChatConfig chatConfig;

    private void Awake()
    {
        instance = this;
    }
}