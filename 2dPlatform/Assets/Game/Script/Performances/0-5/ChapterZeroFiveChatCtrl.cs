using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterZeroFiveChatCtrl : MonoBehaviour
{
    public ChatMonitor defendChatMonitor;
    public ChatMonitor deflectChatMonitor;
    [SerializeField] GameObject chat1;
    [SerializeField] GameObject chat2;
    [SerializeField] GameObject chat3;

    [SerializeField] GameObject slimes;
    [SerializeField] GameObject musoulooms;

    [SerializeField] GameObject goat;

    float defendChatDownCount = 9.0f;
    float deflectChatDownCount = 18.0f;

    bool canSetOne = true;
    bool canSetTwo = true;

    private void Start()
    {
        chat1.SetActive(true);
        chat2.SetActive(false);
        chat3.SetActive(false);
        slimes.SetActive(false);
        musoulooms.SetActive(false);
        goat.SetActive(false);
    }
    private void Update()
    {
        if (canSetOne)
        {
            if (defendChatMonitor.playerIsInDefendChatArea)
            {
                defendChatDownCount -= Time.deltaTime;
                if (defendChatDownCount <= 0)
                {
                    chat1.SetActive(false);
                    if (Input.GetKeyDown(KeyCode.K))
                    {
                        chat2.SetActive(true);
                        canSetOne = false;
                    }
                }
            }
        }


        if (canSetTwo)
        {
            if (deflectChatMonitor.playerIsInDeflectChatIsArea)
            {
                deflectChatDownCount -= Time.deltaTime;
                if (deflectChatDownCount <= 0)
                {
                    chat2.SetActive(false);
                    chat3.SetActive(true);
                    slimes.SetActive(true);
                    musoulooms.SetActive(true);
                    goat.SetActive(true);
                    canSetTwo = false;
                }
            }
        }

    }
}
