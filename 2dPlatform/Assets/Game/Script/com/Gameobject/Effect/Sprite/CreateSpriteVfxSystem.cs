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


        int testIndex = 0;
        public void TestShowreel()
        {
            var p = PlayerBehaviour.instance;
            switch (testIndex)
            {
                case 0:
                    Create("Firelight1", p.defend.deflectArea.transform.position);
                    break;
                case 1:
                    Create("Firelight2", p.defend.deflectArea.transform.position);
                    break;
                case 2:
                    Create("Claw", p.transform.position);
                    break;
                case 3:
                    Create("dmg by es", p.transform.position);
                    break;
                case 4:
                    Create("dmg by es1", p.transform.position);
                    break;
                case 5:
                    Create("dmg by fs", p.transform.position);
                    break;
                case 6:
                    Create("dmg by fs1", p.transform.position);
                    break;
                case 7:
                    Create("dust1", p.transform.position);
                    break;
                case 8:
                    Create("dust2", p.transform.position);
                    break;
                case 9:
                    Create("dust3", p.transform.position);
                    break;
            }

            testIndex++;
            if (testIndex >= 10)
            {
                testIndex = 0;
            }
        }
    }
}