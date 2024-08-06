using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HpBarFixedWidthBehaviour : MonoBehaviour
{
    public float duration = 0.6f;
    public RectTransform bar;
    public RectTransform bar_shadow;

    public CanvasGroup cg;
    public bool hideIfFull;
    public float powerScaleValue = 1;
    public float length;
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
        if (float.IsNaN(percentage)) percentage = 0;
        var endValue = Mathf.Max(0f, Mathf.Min(percentage, 1f));
        if (powerScaleValue != 1)
            endValue = Mathf.Pow(percentage, powerScaleValue);
        if (float.IsNaN(endValue)) endValue = 0;
        var barSize = new Vector2(length * endValue, bar.sizeDelta.y);
        if (bar_shadow != null)
        {
            if (!instant && duration > 0)
            {
                bar_shadow.DOKill();
                bar_shadow.DOSizeDelta(barSize, duration).SetEase(Ease.OutCubic).SetDelay(delay);
            }
            else
            {
                bar_shadow.DOKill();
                bar_shadow.sizeDelta = barSize;
            }
        }

        bar.sizeDelta = barSize;
        if (percentage == 1 && hideIfFull)
        {
            Hide();
            return;
        }

        Show();
    }
}