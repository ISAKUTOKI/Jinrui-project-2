using Mono.Cecil.Cil;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

namespace Assets.Game.Script.GameFlow
{
    public class ReviveSystem : MonoBehaviour
    {
        public static ReviveSystem instance;
        public CanvasGroup cgDie;

        public float dieDelay = 1.6f;

        private float _pendingDieTiming;
        private bool _fromFall;

        public int deathPhase;//0 revive as girl 1 sleep 2 revive as warrior

        public GameObject btnRevive1;
        public GameObject btnRevive2;
        public GameObject btnSleep;

        private void Awake()
        {
            _pendingDieTiming = -1;
            instance = this;
        }

        private void Start()
        {
            Hide();
        }

        void Hide()
        {
            cgDie.alpha = 0;
            cgDie.blocksRaycasts = false;
            btnRevive1.SetActive(false);
            btnRevive2.SetActive(false);
            btnSleep.SetActive(false);
        }

        void Show()
        {
            Hide();
            cgDie.blocksRaycasts = true;

            cgDie.DOFade(1, 1).OnComplete(ShowBtn);
        }

        void ShowBtn()
        {
            if (deathPhase == 0)
            {
                btnRevive1.SetActive(true);
            }
            else if (deathPhase == 1)
            {
                btnSleep.SetActive(true);
            }
            else if (deathPhase == 2)
            {
                btnRevive2.SetActive(true);
            }
        }

        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void LoadSleepScene()
        {
            SceneManager.LoadScene(1);
        }

        private void Update()
        {
            if (_pendingDieTiming > 0)
            {
                if (_pendingDieTiming <= Time.time)
                {
                    _pendingDieTiming = 0;
                    Show();
                }
            }
        }
        public void QueueDie(bool fromFall = false)
        {
            if (_pendingDieTiming < 0)
            {
                if (deathPhase == 1)
                {
                    _pendingDieTiming = Time.time + 0f;
                    return;
                }

                _fromFall = fromFall;
                if (_fromFall)
                    _pendingDieTiming = Time.time + dieDelay * 0.1f;
                else
                    _pendingDieTiming = Time.time + dieDelay;
            }
        }
    }
}