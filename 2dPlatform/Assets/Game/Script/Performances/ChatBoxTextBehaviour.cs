using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatBoxTextMeshBehaviour : MonoBehaviour
{
    public static ChatBoxTextMeshBehaviour instance;
    [HideInInspector]public TextMeshPro textMeshPro;

    private void Awake()
    {
        instance = this;
        ClearText();
    }
    private void Start()
    {
        textMeshPro = GetComponent<TextMeshPro>();
    }

    public void SetText(string text)
    {
        textMeshPro.text = text;
    }
    public void ClearText()
    {
        textMeshPro.text = "";
    }
}
