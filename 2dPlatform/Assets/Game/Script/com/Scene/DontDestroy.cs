using UnityEngine;
using UnityEngine.SceneManagement;

namespace com
{
    public class DontDestroy : MonoBehaviour
    {
        void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}

