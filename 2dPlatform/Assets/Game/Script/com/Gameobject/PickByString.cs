using UnityEngine;
using System.Collections.Generic;

namespace com
{
    [System.Serializable]
    public struct pickInfo
    {
        public List<string> ids;
        public List<GameObject> gos;

        public void Close()
        {
            foreach (var go in gos)
            {
                go.SetActive(false);
            }
        }
    }

    public class PickByString : MonoBehaviour
    {
        public pickInfo[] infos;
        public pickInfo defaultInfo;

        public void Setup(string s)
        {
            foreach (var info in infos)
            {
                //info.Close();
                if (!info.ids.Contains(s))
                    continue;
                SetToInfo(info);
                return;
            }

            SetToInfo(defaultInfo);
        }

        private void SetToInfo(pickInfo info)
        {
            if (info.gos.Count == 1)
            {
                SetActive(info.gos[0]);
            }
            else if (info.gos.Count > 1)
            {
                int r = Random.Range(0, info.gos.Count);
                SetActive(info.gos[r]);
            }
        }

        void SetActive(GameObject go)
        {
            go.SetActive(true);
        }
    }
}