using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HpBarBehaviour : MonoBehaviour
{
    public float duration = 0.6f;
    public Image bar;
    public Image bar_shadow;

    public RectTransform rectTrans;
    public CanvasGroup cg;
    public bool hideIfFull;
    public float powerScaleValue = 1;
    public float delay;
    public void Hide()
    {
        cg.alpha = 0;
    }

    public void Show()
    {
        cg.alpha = 1;
    }

    public void Set(float percentage, bool instant = false)
    {
        if (percentage == 1 && hideIfFull)
        {
            Hide();
            return;
        }

        var endValue = percentage;
        if (powerScaleValue != 1)
            endValue = Mathf.Pow(percentage, powerScaleValue);

        if (bar_shadow != null)
        {
            if (!instant && duration > 0)
            {
                bar_shadow.DOKill();
                bar_shadow.fillAmount = bar.fillAmount;
                bar_shadow.DOFillAmount(endValue, duration).SetEase(Ease.OutCubic).SetDelay(delay);
            }
            else
                bar_shadow.fillAmount = endValue;
        }

        bar.fillAmount = endValue;
        Show();
    }
}