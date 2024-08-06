using UnityEngine;
using System.Collections.Generic;

namespace com
{
    public class StartActive : MonoBehaviour
    {
        public List<GameObject> gos;

        void Start()
        {
            foreach (var go in gos)
                go.SetActive(true);
        }
    }
}
