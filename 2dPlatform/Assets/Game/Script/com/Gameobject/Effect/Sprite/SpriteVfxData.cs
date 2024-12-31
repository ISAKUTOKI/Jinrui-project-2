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
        public bool loop = false;
        public float frameTime = 0.04f;
        public Sprite[] sps;
        public bool autoDestroy = true;
        public Action endCallback;
        public float scale = 1f;
    }
}