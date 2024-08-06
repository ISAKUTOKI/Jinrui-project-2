using UnityEngine;
using DG.Tweening;

namespace com
{
    public class CinematicCameraService : CinematicService
    {
        public Camera targetCamera;

        public static CinematicCameraService instance_camera { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            instance_camera = this;
        }

        protected override void Start()
        {
            base.Start();

            if (targetCamera == null)
                targetCamera = target.GetComponent<Camera>();
        }

        protected override void TriggerEvent(CinematicEventPrototype proto)
        {
            base.TriggerEvent(proto);

            var duration = proto.duration;
            if (duration < 0)
                duration = proto.TimeToNext;

            switch (proto.type)
            {
                case CinematicActionTypes.TweenFov:
                    targetCamera.DOFieldOfView(proto.floatParam, duration).SetEase(proto.ease);
                    break;
            }
        }
    }
}