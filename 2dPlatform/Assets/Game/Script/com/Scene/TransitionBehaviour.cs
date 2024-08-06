using UnityEngine;
using System;
using DG.Tweening;

namespace com
{
    public class TransitionBehaviour : MonoBehaviour
    {
        public float sizeMin;
        public float sizeMax;
        public float duration;
        public float durationSmall;
        public float rotationDelta;

        public CanvasGroup cg;
        public RectTransform view;
        private Action _cb;

        public static TransitionBehaviour instance { get; private set; }

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            //Hide();
        }

        public void SetCallback(Action cb = null)
        {
            _cb = cb;
        }

        public void Hide()
        {
            //Debug.Log("Hide");
            cg.alpha = 0;
            SetCallback();
        }

        public void Opening(Action cb = null)
        {
            SetCallback(cb);
            cg.DOKill();
            cg.alpha = 1;
            cg.DOFade(0, duration).OnComplete(() =>
            {
                _cb?.Invoke();
            });
        }

        public void ShowBigger(Action cb = null)
        {
            //Debug.Log("ShowBigger");
            StartTransition(duration, cb);
        }

        public void ShowSmaller(Action cb = null)
        {
            //Debug.Log("ShowSmaller");
            StartTransition(durationSmall, cb);
        }

        void StartTransition(float t, Action cb)
        {
            //Debug.Log("StartTransition");
            SetCallback(cb);
            cg.DOKill();
            cg.alpha = 0;
            cg.DOFade(1, t).OnComplete(() =>
            {
                _cb?.Invoke();
                cg.DOFade(0, t);
            });
        }

        void StartTransitionRotate(float start, float end, float t, Action cb)
        {
            //Debug.Log("StartTransition");
            SetCallback(cb);
            view.localScale = new Vector3(start, start, 1);
            view.DOKill();
            view.DOScale(new Vector3(end, end, 1), t).SetEase(Ease.InOutCubic);
            view.DORotate(new Vector3(0, 0, rotationDelta), t, RotateMode.FastBeyond360).SetEase(Ease.InOutCubic).OnComplete(() =>
            {
                //Debug.Log("cb");
                _cb?.Invoke();
                //Hide();
            });
            cg.alpha = 1;
        }
    }
}