using com;
using System.Collections;
using UnityEngine;


public class Destroyable : MonoBehaviour
{
    public GameObject vfx;
    public string sfx;

    public void OnDestroy()
    {
        com.SoundSystem.instance.Play(sfx);
        Instantiate(vfx, transform.position, transform.rotation);
    }

}