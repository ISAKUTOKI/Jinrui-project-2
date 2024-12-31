using System;
using System.Collections;
using UnityEngine;

namespace com
{
    [System.Serializable]
    public class SpriteVfxData
    {
        public string id;
        public Vector2 offset;
        public bool loop;
        public float frameTime;
        public Sprite[] sps;
        public bool autoDestroy;
        public Action endCallback;
    }
}