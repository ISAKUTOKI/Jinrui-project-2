using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TextMeshProUGUI))]
public class DialogTextAnimation : MonoBehaviour
{
    public bool Finished { get; private set; }
    public bool Skipping { get; private set; }

    public CanvasGroup CG;

    public GameObject Arrow;//triangle
    public float characterPerSecond = 20;
    public float characterPerSecondPass = 36;
    private TextMeshProUGUI _targetTextLabel;
    private float lettersShown = 0;
    private int maxLettersCount = 200;

    private void Awake()
    {
        _targetTextLabel = GetComponent<TextMeshProUGUI>();
    }

    public void StartAnimation()
    {
        Arrow.SetActive(false);
        _targetTextLabel.maxVisibleCharacters = 0;
        lettersShown = 0;
        Finished = false;
        Skipping = false;
        maxLettersCount = 200;
    }

    public void Skip()
    {
        Skipping = true;
    }

    private void Update()
    {
        if (Finished) return;
        if (CG.alpha < 1f || !gameObject.activeInHierarchy) return;

        //先调用 _text.ForceMeshUpdate();更新文本，才能获得真正的长度，忽略richtext的tag
        //https://forum.unity.com/threads/problem-with-gettextinfo-string-text.645247/

        //maxLettersCount = _targetTextLabel.textInfo.characterCount;
        maxLettersCount = _targetTextLabel.textInfo.characterCount;

        if (Skipping)
        {
            lettersShown += Time.unscaledDeltaTime * characterPerSecondPass;
        }
        else
        {
            lettersShown += Time.unscaledDeltaTime * characterPerSecond;
        }
        if (lettersShown >= maxLettersCount)
        {
            Finished = true;
            Arrow.SetActive(true);
        }

        _targetTextLabel.maxVisibleCharacters = (int)lettersShown;
    }
}