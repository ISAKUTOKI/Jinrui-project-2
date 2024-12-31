using System;
using System.Collections;
using UnityEngine;

namespace com
{
    public class CreateSpriteVfxSystem : MonoBehaviour
    {
        public SpriteVfxInstance prefab;
        public static CreateSpriteVfxSystem instance;
        public SpriteVfxData[] datas;
        public Transform spawnParent;

        private void Awake()
        {
            instance = this;
        }

        public void Create(string id, Vector2 pos)
        {
            foreach (var d in datas)
            {
                if (d.id == id)
                {
                    Create(d, pos);
                    return;
                }
            }
        }

        public void Create(int index, Vector2 pos)
        {
            Create(datas[index], pos);
        }

        public void Create(SpriteVfxData data, Vector2 pos)
        {
            var svi = Instantiate(prefab, spawnParent);
            svi.data = data;
            svi.transform.position = pos + data.offset;
        }
    }
}