using UnityEngine;
using UnityEngine.UI;
using Text = TMPro.TextMeshProUGUI;
using com;
using DG.Tweening;

public class ChatPanelBehaviour : MonoBehaviour
{
    private Text _text;
    public Image right;
    public Image left;
    public CanvasGroup cg;
    private DialogTextAnimation _textAnimation;
    public static ChatPanelBehaviour instance { get; private set; }

    private void Awake()
    {
        instance = this;

        _text = GetComponentInChildren<Text>();
        _textAnimation = _text.GetComponent<DialogTextAnimation>();
        Hide();
    }

    public void Hide()
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
        _text.SetText("");
    }

    public void Show(ChatPrototype chat)
    {
        string sound = chat.soundName;
        if (string.IsNullOrEmpty(sound))
            sound = "chat";

        SoundSystem.instance.Play(sound);

        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;

        _text.DOKill();
        _text.SetText(chat.content);
        _text.ForceMeshUpdate();
        _textAnimation.StartAnimation();

        right.enabled = false;
        left.enabled = false;
        var chatCharacter = ConfigSystem.instance.chatConfig.GetChatCharacterPrototype(chat.characterId);
        if (chatCharacter != null)
        {
            var sp = chatCharacter.sp;
            if (sp != null)
            {
                if (chatCharacter.isRightSide)
                {
                    right.enabled = true;
                    right.sprite = sp;
                }
                else
                {
                    left.enabled = true;
                    left.sprite = sp;
                }
            }
        }
    }

    public void UserTapped()
    {
        if (_textAnimation.Finished)
        {
            ChatSystem.instance.OnChatEnd();
        }
        else
        {
            _textAnimation.Skip();
        }
    }
}