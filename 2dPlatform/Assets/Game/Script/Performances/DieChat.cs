using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;

public class DieChat : MonoBehaviour
{
    public static DieChat instance;

    [SerializeField] List<ChatTextInfo> chat = new List<ChatTextInfo>();
    [SerializeField] bool isPauseChat = false;
    //[SerializeField] bool isExitEndChat = false;
    //[SerializeField] bool canChatAgain = false;
    [SerializeField] bool isSafeChat = false;
    [SerializeField] TMP_FontAsset targetFont;
    Coroutine currentCoroutine;

    [HideInInspector] public int DieCount;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Invoke("FirstDiedChat", 0.1f);
        //Debug.Log("Scene Loaded: " + scene.name);
        //Debug.Log("Load Mode: " + mode);
        Debug.Log("ËÀ¹ýÁË " + DieCount + " ´Î");
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void FirstDiedChat()
    {
        if (DieCount == 1)
        {
            if (isPauseChat)
            {
                PlayerBehaviour.instance.animator.SetBool("walk", false);
                PlayerBehaviour.instance.movePosition.StopXMovement();
                PlayerBehaviour.instance.move.canMove = false;
            }
            if (isSafeChat)
            {
                PlayerBehaviour.instance.health.canBeWounded = false;
            }
            ChatBoxSystem.instance.IWantTalk();
            currentCoroutine = StartCoroutine(PlayFirstDieChatTextCoroutine());
        }
    }

    private IEnumerator PlayFirstDieChatTextCoroutine()
    {
        ChatBoxTextMeshBehaviour.instance.SetFont(targetFont);
        yield return new WaitForSeconds(0.2f);
        if (chat != null)
        {
            foreach (var item in chat)
            {
                item.InvokeChatBoxAction();
                ChatBoxTextMeshBehaviour.instance.SetText(item.text);
                yield return new WaitForSeconds(item.duration);
                //Debug.Log(isPauseChat);
            }
        }
        if (chat.Count > 0)
        {
            var lastChatAction = chat[chat.Count - 1].chatBoxAction;
            if (lastChatAction != ChatTextInfo.ChatBoxAction.Stop && lastChatAction != ChatTextInfo.ChatBoxAction.FastStop)
            {
                ChatBoxSystem.instance.IWantStop();
            }
        }

        ChatBoxTextMeshBehaviour.instance.SetBackToLastFont();
        PlayerBehaviour.instance.move.canMove = true;
        PlayerBehaviour.instance.health.canBeWounded = true;
    }
}
