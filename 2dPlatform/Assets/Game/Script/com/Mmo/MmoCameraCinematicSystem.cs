using UnityEngine;

namespace com
{
    public class MmoCameraCinematicSystem : MonoBehaviour
    {
        public static MmoCameraCinematicSystem instance { get; private set; }

        public MmoCameraBehaviour cam;

        private void Awake()
        {
            instance = this;
        }

        void DisablePlayerCamera()
        {
            cam.enabled = false;
        }

        void EnablePlayerCamera()
        {
            cam.enabled = true;
        }
    }
}