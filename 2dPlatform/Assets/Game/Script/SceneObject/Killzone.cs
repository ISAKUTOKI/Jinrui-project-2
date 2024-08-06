using System.Collections;
using UnityEngine;

namespace Assets.Game.Script.SceneObject
{
    public class Killzone : MonoBehaviour
    {
        public bool hideSprite;

        // Use this for initialization
        void Start()
        {
            var sp = GetComponent<SpriteRenderer>();
            if (hideSprite && sp)
                sp.enabled = false;
        }
    }
}