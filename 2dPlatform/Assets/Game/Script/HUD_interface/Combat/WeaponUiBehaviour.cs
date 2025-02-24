using Assets.Game.Script.HUD_interface.Combat;
using com;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUiBehaviour : MonoBehaviour
{
    // empty p1 p2 p3
    public UiImageAnimation uia;

    public WeaponUiPart p1;
    public WeaponUiPart p2;
    public WeaponUiPart p3;

    public GameObject view;

    [Serializable]
    public class WeaponUiPart
    {
        public Image img;
        public Image bg;
        public void SetEmpty()
        {
            img.fillAmount = 0;
        }

        public void SetFull()
        {
            img.fillAmount = 1;
        }

        public void SetPercent(float p)
        {
            float a = 0.04f + p * 0.91f;
            img.fillAmount = a;
        }
    }

    private void Start()
    {
        uia.Stop();
    }
    private void Update()
    {
        if (PlayerBehaviour.instance.getSword.hasGotSword)
        {
            return;
        }
        else
        {
            view.SetActive(false);
        }
    }
    public void SyncPowerValue(float v)
    {
        uia.Stop();
        uia.SetPlayEndCallback(null);
        p1.bg.enabled = true;
        p1.img.enabled = true;
        p2.bg.enabled = true;
        p2.img.enabled = true;
        p3.bg.enabled = true;
        p3.img.enabled = true;
        p3.img.enabled = true;
        if (v == 3)
        {
            p1.SetFull();
            p2.SetFull();
            p3.SetFull();
        }
        else if (v > 2 && v < 3)
        {
            p1.SetFull();
            p2.SetFull();
            p3.SetPercent(v - 2);
        }
        else if (v == 2)
        {
            p1.SetFull();
            p2.SetFull();
            p3.SetEmpty();
        }
        else if (v > 1 && v < 2)
        {
            p1.SetFull();
            p2.SetPercent(v - 1);
            p3.SetEmpty();
        }
        else if (v == 1)
        {
            p1.SetFull();
            p2.SetEmpty();
            p3.SetEmpty();
        }
        else if (v > 0 && v < 1)
        {
            p1.SetPercent(v - 0);
            p2.SetEmpty();
            p3.SetEmpty();
        }
        else if (v == 0)
        {
            p1.SetEmpty();
            p2.SetEmpty();
            p3.SetEmpty();
        }
    }

    /// <summary>
    ///  use power : attack consume 1 cell
    ///  non full cell: when use 1 cell, use the non interger part
    /// </summary>
    /// <param name="p"></param>

    public void TogglePowerFullAnim(bool playOrStop)
    {
        if (playOrStop)
            uia.Play(4);
        else
            uia.Stop();
    }

    public void PlayP0_P1Anim()
    {
        p1.bg.enabled = false;
        p1.img.enabled = false;
        uia.Play(1);
        uia.SetPlayEndCallback(
       () =>
       {
           p1.bg.enabled = true;
           p1.img.enabled = true;
       }
       );
    }

    public void PlayP1_P2Anim()
    {
        p2.bg.enabled = false;
        p2.img.enabled = false;
        uia.Play(2);
        uia.SetPlayEndCallback(
          () =>
          {
              p2.bg.enabled = true;
              p2.img.enabled = true;
          }
          );
    }

    public void PlayP2_P3Anim()
    {
        p3.bg.enabled = false;
        p3.img.enabled = false;
        uia.Play(3);
        uia.SetPlayEndCallback(
           () =>
           {
               TogglePowerFullAnim(true);
               p3.bg.enabled = true;
               p3.img.enabled = true;
           }
           );
    }

    public void PlayPMaxAnim()
    {
        uia.Play(0);
        p1.bg.enabled = false;
        p1.img.enabled = false;
        p2.bg.enabled = false;
        p2.img.enabled = false;
        p3.bg.enabled = false;
        p3.img.enabled = false;
        uia.SetPlayEndCallback(
            () =>
            {
                TogglePowerFullAnim(true);
                p1.bg.enabled = true;
                p1.img.enabled = true;
                p2.bg.enabled = true;
                p2.img.enabled = true;
                p3.bg.enabled = true;
                p3.img.enabled = true;
                p3.img.enabled = true;
            }
            );
    }
}