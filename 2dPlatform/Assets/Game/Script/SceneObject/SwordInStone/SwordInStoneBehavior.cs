using System.Collections;
using UnityEngine;

public class SwordInStoneBehavior : MonoBehaviour
{
    Animator animator;
    public GameObject Neko;
    public Camera Camera;
    public GameObject Chat;
    public GameObject Goat;
    public GameObject notGetSwordChat;
    public SIS_JKey JKey { get; private set; }
    bool canTryToGetSword = false;

    //�ν�����
    int tapCounter = 0;
    int tapTargetCount = 13;
    bool isChecking = false;
    bool canFirstCheck = true;
    bool checkSucceeded = false;
    float getCheckTimer = 0f;
    float getCheckDuration = 2.0f;

    //��������ű���
    bool cameraCanBackToInitial = false;
    float cameraInitialSize;
    float cameraCurrentSize;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        JKey = GetComponentInChildren<SIS_JKey>();
        CameraInitialSet();//��ʼ�����
    }

    void Update()
    {
        if (canTryToGetSword)
            TapKeyCheck();
        ChangeChatEnable();
    }

    /// <summary>
    /// Neko������ײ���߼�
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == Neko && !checkSucceeded)
        {
            canTryToGetSword = true;
            JKey.seeable = true;
            JKey.canSelfRotate = true;
            JKey.canSelfScale = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == Neko)
        {
            canTryToGetSword = false;
            JKey.seeable = false;
            JKey.canSelfRotate = false;
            JKey.canSelfScale = false;
            if (isChecking)
                FailToGetSword();
        }
    }

    void TapKeyCheck()
    {
        if (Input.GetKeyDown(KeyCode.J) && canFirstCheck && !checkSucceeded)
        {
            isChecking = true;
            canFirstCheck = false;
        }//�������J���ҿ����״Σ��ͽ�����

        if (isChecking)
        {
            IsGettingSword();
            getCheckTimer += Time.deltaTime;

            if (getCheckTimer >= getCheckDuration)
                FailToGetSword();

            if (Input.GetKeyDown(KeyCode.J) && !canFirstCheck)
            {
                tapCounter++;
                if (tapCounter >= tapTargetCount)
                    SwordIsGot();
            }
        }//������ڼ��״̬���ٰ�J�Ϳ��Լ���
    }

    void IsGettingSword()
    {
        cameraCanBackToInitial = false;
        animator.SetBool("isGetting", true);
        CameraTapScale();
        JKey.canSelfRotate = false;
        JKey.canSelfScale = false;
        JKey.isTappingKey = true;
    }//Ӱ�춯�������

    void FailToGetSword()
    {
        cameraCanBackToInitial = true;
        getCheckTimer = 0;
        tapCounter = 0;
        isChecking = false;
        animator.SetBool("isGetting", false);
        JKey.canSelfRotate = true;
        JKey.canSelfScale = true;
        JKey.isTappingKey = false;
        StartCoroutine(CameraTryBackToInitialCoroutine());
        StartCoroutine(FailedTimerCoroutine(1.5f));
    }//���ñ�����Ӱ�����

    IEnumerator FailedTimerCoroutine(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        canFirstCheck = true;
    }//Э�̣�һ��ʱ���ſ����ٴΰν�

    void SwordIsGot()
    {
        canFirstCheck = false;
        cameraCanBackToInitial = true;
        checkSucceeded = true;
        animator.SetTrigger("got");
        JKey.seeable = false;
        StartCoroutine(CameraTryBackToInitialCoroutine());
        Invoke("DelayChangePLayerPhase", 1.5f);
    }//�ν��ɹ����趨����

    void DelayChangePLayerPhase()
    {
        PlayerBehaviour.instance.getSword.hasGotSword = true;
    }
    void CameraInitialSet()
    {
        cameraInitialSize = Camera.orthographicSize;
        cameraCurrentSize = cameraInitialSize;
    }//�����ʼ�ߴ��¼������

    void CameraTapScale()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            StartCoroutine(CameraTapScaleCoroutine(0));
        }
    }//��J�������

    IEnumerator CameraTapScaleCoroutine(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        cameraCurrentSize *= 0.97f;
        Camera.orthographicSize = cameraCurrentSize;
    }//���������Э��

    IEnumerator CameraTryBackToInitialCoroutine()
    {
        if (cameraCanBackToInitial)
        {
            if (checkSucceeded)
                yield return new WaitForSeconds(1.5f);

            Camera.orthographicSize = cameraInitialSize;
            cameraCurrentSize = cameraInitialSize;
        }
    }//�����λ��Э��

    void ChangeChatEnable()
    {
        if (checkSucceeded)
        {
            Chat.SetActive(true);
            notGetSwordChat.SetActive(false);
            Goat.SetActive(true);
        }
        else
        {
            Chat.SetActive(false);
            notGetSwordChat.SetActive(true);
            Goat.SetActive(false);
        }
    }
}