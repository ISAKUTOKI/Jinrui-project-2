using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HpHalfHeartsBehaviour : MonoBehaviour
{
    public HpHalfHeartBehaviour prefab;

    public RectTransform parent;

    [SerializeField] List<HpHalfHeartBehaviour> _hearts = new List<HpHalfHeartBehaviour>();

    public void Init(int maxHeartCount)
    {
        for (int i = 0; i < maxHeartCount; i++)
        {
            var h = Instantiate(prefab, parent);
            _hearts.Add(h);
        }
    }

    public void FullFillAll()
    {
        foreach (var h in _hearts)
            h.SetWhole();
    }

    public void SetHp(int newHp, bool withAnimeOnReducing)
    {
        int currentContainHp = 0;

        foreach (var h in _hearts)
        {
            var crtContainHp = h.containHp;
            currentContainHp += crtContainHp;
            int delta = currentContainHp - newHp;
            if (crtContainHp == 2 && delta == 1)
            {
                //2 to 1
                h.SetHalf(withAnimeOnReducing);
            }
            else if (crtContainHp == 2 && delta >= 2)
            {
                //2 to 0
                h.SetEmpty(withAnimeOnReducing);
            }
            else if (crtContainHp == 1 && delta >= 1)
            {
                //1 to 0
                h.SetEmpty(withAnimeOnReducing);
            }
            else if (crtContainHp == 1 && delta <= -1)
            {
                //1 to 2
                h.SetWhole();
            }
            else if (crtContainHp == 0 && delta == -1)
            {
                //0 to 1
                h.SetHalf(withAnimeOnReducing);
            }
            else if (crtContainHp == 0 && delta <= -2)
            {
                //0 to 2
                h.SetWhole();
            }
        }
    }
}