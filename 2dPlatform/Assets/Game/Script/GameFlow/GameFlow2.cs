using Assets.Game.Script.GameFlow;
using com;
using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlow2 : GameFlowSystem
{
    public ChatPrototype[] chats;
    public CanvasGroup blackScreen;
    public Transform 宝箱的石墙;
    void Start()
    {
        //ToggleBossHpBar(false);
        ReviveSystem.instance.deathPhase = 0;
        blackScreen.alpha = 1;
        TogglePlayerControl(false);
        TogglePlayerHpBar(false);
        StartCoroutine(Cinematic_山洞());
    }

    IEnumerator PlayerBecomeWarrior()
    {
        TogglePlayerControl(false);
        yield return new WaitForSeconds(1);

        SoundSystem.instance.Play("change");
        psChange.Play(true);
        yield return new WaitForSeconds(0.2f);
        PlayerBehaviour.instance.ToggleWarrierState(true);
        yield return new WaitForSeconds(1);

        ChatSystem.instance.ShowChat(chats[2]);
        while (ChatSystem.instance.flag != "change done")
            yield return null;

        character.girl.SetAnimTrigger("idleVariant");
        yield return new WaitForSeconds(2.3f);
        character.girl.SetAnimTrigger("attack");
        yield return new WaitForSeconds(1f);
        TogglePlayerControl(true);

        ReviveSystem.instance.deathPhase = 2;

        while (ChatSystem.instance.flag != "next level")
            yield return null;

        SceneManager.LoadScene(2);
    }

    public ParticleSystem psChange;

    IEnumerator Cinematic_山洞()
    {
        yield return new WaitForSeconds(1);
        ChatSystem.instance.ShowChat(chats[0]);

        while (true)
        {
            yield return null;
            if (ChatSystem.instance.flag == "wake up")
            {
                break;
            }
        }

        blackScreen.DOFade(0, 3);

        yield return new WaitForSeconds(1);
        TogglePlayerControl(true);


        while (true)
        {
            yield return null;
            if (ChatSystem.instance.flag == "chest down")
            {
                break;
            }
        }
        宝箱的石墙.DOLocalMoveY(71.6f, 3).SetEase(Ease.InOutCubic);
        yield return new WaitForSeconds(3);

        ChatSystem.instance.ShowChat(chats[1]);
        while (true)
        {
            yield return null;
            if (ChatSystem.instance.flag == "change")
            {
                break;
            }
        }

        StartCoroutine(PlayerBecomeWarrior());
    }
}