using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace com
{
    public class ClippingService : MonoBehaviour
    {
        public bool IsCulling;
        public float magnitudeClipping = 2;
        public static ClippingService Instance;

        void Awake()
        {
            Instance = this;
        }

        public void Clipping()
        {
        }
    }
}