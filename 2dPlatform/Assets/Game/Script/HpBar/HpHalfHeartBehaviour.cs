using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using com;
using System.Collections;
using System;
using System.ComponentModel;

public class HpHalfHeartBehaviour : MonoBehaviour
{
    [SerializeField] float blinkInterval = 0.04f;
    [SerializeField] float blinkTurns = 4;
    [SerializeField] Image blinker_half;
    [SerializeField] Image blinker_whole;
    [SerializeField] Image view_half;
    [SerializeField] Image view_whole;
    [SerializeField] UiImageAnimation anime;
    public int containHp { get; private set; }

    private void Start()
    {
        anime.SetPlayEndCallback(() => { anime.ToggleDisplay(false); });
        HideBlinkers();
    }

    public void SetWhole()
    {
        containHp = 2;
        view_half.enabled = false;
        view_whole.enabled = true;
        anime.ToggleDisplay(false);
    }

    public void SetHalf(bool hasAnime)
    {
        containHp = 1;
        if (!hasAnime)
        {
            view_half.enabled = true;
            view_whole.enabled = false;
            anime.ToggleDisplay(false);
            return;
        }

        anime.ToggleDisplay(true);
        anime.Play(0);
        HideBlinkers();
        StartCoroutine(Blink(blinker_whole,
            () =>
            {
                view_half.enabled = true;
                view_whole.enabled = false;
            }));
    }

    public void SetEmpty(bool hasAnime)
    {
        containHp = 0;
        if (!hasAnime)
        {
            view_half.enabled = false;
            view_whole.enabled = false;
            anime.ToggleDisplay(false);
            return;
        }

        anime.ToggleDisplay(true);
        anime.Play(0);
        HideBlinkers();
        StartCoroutine(Blink(blinker_half,
            () =>
            {
                view_half.enabled = false;
                view_whole.enabled = false;
            }));
    }

    IEnumerator Blink(Image b, Action cb)
    {
        var c = blinkTurns;
        while (c >= 0)
        {
            b.enabled = true;
            yield return new WaitForSeconds(blinkInterval);
            b.enabled = false;
            yield return new WaitForSeconds(blinkInterval);
            c--;
        }

        cb?.Invoke();
    }

    void HideBlinkers()
    {
        StopAllCoroutines();
        blinker_half.enabled = false;
        blinker_whole.enabled = false;
    }
}