using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

namespace com
{
    public class CinematicService : MonoBehaviour
    {
        public Transform target;
        public List<CinematicEventPrototype> events;

        protected float _nextTimestamp;
        protected int _crtEventIndex;

        public static CinematicService instance { get; private set; }

        protected virtual void Awake()
        {
            instance = this;
        }

        protected virtual void Start()
        {
            _nextTimestamp = -1;
            ResetEvents();
        }

        public void StartService()
        {
            _nextTimestamp = Time.time;
            _crtEventIndex = -1;
        }

        public void ResetEvents()
        {
            events = new List<CinematicEventPrototype>();
        }

        public void AddEvents(CinematicEventPrototype e)
        {
            events.Add(e);
        }
        // Update is called once per frame
        void Update()
        {
            TryTriggerEvent();
        }

        void TryTriggerEvent()
        {
            if (_nextTimestamp > 0 && Time.time >= _nextTimestamp)
            {
                TriggerNextEvent();
            }
        }

        void TriggerNextEvent()
        {
            var index = _crtEventIndex + 1;
            if (index > events.Count - 1)
                return;

            _crtEventIndex = index;
            var crtEvent = events[_crtEventIndex];
            _nextTimestamp = Time.time + crtEvent.TimeToNext;

            if (_crtEventIndex + 1 > events.Count - 1)
            {
                _nextTimestamp = -1;
                //Debug.Log("no next event");
            }

            TriggerEvent(crtEvent);
        }

        protected virtual void TriggerEvent(CinematicEventPrototype proto)
        {
            //Debug.Log("TriggerEvent");
            var duration = proto.duration;
            if (duration < 0)
                duration = proto.TimeToNext;

            switch (proto.type)
            {
                case CinematicActionTypes.None:
                    break;

                case CinematicActionTypes.SetPosition:
                    if (proto.usePositionAndRotation)
                    {
                        target.position = proto.position;
                    }
                    else
                    {
                        target.position = proto.trans.position;
                    }
                    break;

                case CinematicActionTypes.SetRotation:
                    if (proto.usePositionAndRotation)
                    {
                        target.rotation = proto.rotation;
                    }
                    else
                    {
                        target.rotation = proto.trans.rotation;
                    }
                    break;

                case CinematicActionTypes.SetPositionAndRotation:
                    if (proto.usePositionAndRotation)
                    {
                        target.position = proto.position;
                        target.rotation = proto.trans.rotation;
                    }
                    else
                    {
                        target.position = proto.trans.position;
                        target.rotation = proto.trans.rotation;
                    }

                    break;

                case CinematicActionTypes.TweenPosition:
                    if (proto.usePositionAndRotation)
                    {
                        target.DOMove(proto.position, duration).SetEase(proto.ease);
                    }
                    else
                    {
                        target.DOMove(proto.trans.position, duration).SetEase(proto.ease);
                    }
                    break;

                case CinematicActionTypes.TweenRotation:
                    if (proto.usePositionAndRotation)
                    {
                        target.DORotate(proto.rotation.eulerAngles, duration).SetEase(proto.ease);
                    }
                    else
                    {
                        target.DORotate(proto.trans.eulerAngles, duration).SetEase(proto.ease);
                    }
                    break;

                case CinematicActionTypes.TweenPositionAndRotation:
                    if (proto.usePositionAndRotation)
                    {
                        target.DOMove(proto.position, duration).SetEase(proto.ease);
                        target.DORotate(proto.rotation.eulerAngles, duration).SetEase(proto.ease);
                    }
                    else
                    {
                        target.DOMove(proto.trans.position, duration).SetEase(proto.ease);
                        target.DORotate(proto.trans.eulerAngles, duration).SetEase(proto.ease);
                    }

                    break;

                case CinematicActionTypes.CallFunc:
                    proto.action?.Invoke();
                    break;

                case CinematicActionTypes.KillTween:
                    target.DOKill();
                    break;

                case CinematicActionTypes.Shake:
                    target.DOShakePosition(duration, proto.floatParam, 10).SetEase(proto.ease);
                    break;

            }
        }
    }
}