using UnityEngine;

[SerializeField]public class ChatTextInfo
{
    public string text;
    public float pauseTime;

    public ChatTextInfo(string t, float p)
    {
        text = t;
        pauseTime = p;
    }
}
