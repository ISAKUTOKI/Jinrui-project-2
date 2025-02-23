using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatBoxTextMeshBehaviour : MonoBehaviour
{
    public static ChatBoxTextMeshBehaviour instance;
    public TextMeshProUGUI textMeshProUGUI { get; private set; }

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        ClearText();
    }


    public void SetText(string text)
    {
        textMeshProUGUI.text = text;
    }
    public void ClearText()
    {
        textMeshProUGUI.text = "";
    }

    TMP_FontAsset lastFont;

    public void SetFont(TMP_FontAsset targetFont)
    {
        lastFont = textMeshProUGUI.font;
        textMeshProUGUI.font = targetFont;
    }

    public void SetBackToLastFont()
    {
        textMeshProUGUI.font = lastFont;
    }
}
