using DG.Tweening;
using UnityEngine.UI;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    public static CombatSystem instance;

    Vector3 _shakeParentStartPos;
    [SerializeField] Image _bloodVignette;
     public Transform shakeParent;
    [SerializeField] float _shakeWeakAmplitude;
    [SerializeField] float _shakeMidAmplitude;
    [SerializeField] float _shakeStrongAmplitude;

    private void Awake()
    {
        instance = this;
    }

    public void ShakeStrong()
    {
        Shake(0.4f, _shakeStrongAmplitude, 20);
    }
    public void ShakeMid()
    {
        Shake(0.3f, _shakeMidAmplitude, 16);
    }
    public void ShakeWeak()
    {
        Shake(0.2f, _shakeWeakAmplitude, 12);
    }
    void Shake(float t, float a, int v)
    {
        shakeParent.DOKill();
        shakeParent.DOShakePosition(t, a, v).OnComplete
            (() => { shakeParent.position = _shakeParentStartPos; });
    }

    public void BloodWeak()
    {
        _bloodVignette.DOKill();
        _bloodVignette.DOFade(0.3f, 0.15f).OnComplete(
            () => { _bloodVignette.DOFade(0f, 0.15f); }
            );
    }

    public void BloodStrong()
    {
        _bloodVignette.DOKill();
        _bloodVignette.DOFade(1f, 0.2f).OnComplete(
            () => { _bloodVignette.DOFade(0f, 0.2f); }
            );
    }

}