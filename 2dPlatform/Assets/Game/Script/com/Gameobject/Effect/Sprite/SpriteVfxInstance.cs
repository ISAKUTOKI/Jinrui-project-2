using System;
using System.Collections;
using UnityEngine;
namespace com
{
    public class SpriteVfxInstance : MonoBehaviour
    {
        public SpriteVfxData data;

        private int _index;

        private SpriteRenderer _sr;

        void Start()
        {
            _index = 0;
            _sr = GetComponent<SpriteRenderer>();
            transform.localScale = data.scale * Vector3.one;
            StartCoroutine(Co());
        }

        IEnumerator Co()
        {
            bool playing = true;
            while (playing)
            {
                _sr.sprite = data.sps[_index];
                yield return new WaitForSeconds(data.frameTime);
                _index++;
                if (_index >= data.sps.Length)
                {
                    if (data.loop)
                    {
                        _index = 0;
                    }
                    else
                    {
                        playing = false;
                    }
                }
            }

            data.endCallback?.Invoke();

            if (data.autoDestroy)
                Destroy(gameObject);
        }
    }
}