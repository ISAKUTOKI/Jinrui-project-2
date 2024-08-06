using System.Collections;
using UnityEngine;

public class HideWhenPlay : MonoBehaviour
{
    void Start()
    {
        if (GetComponent<MeshRenderer>() != null)
            GetComponent<MeshRenderer>().enabled = false;
        if (GetComponent<SpriteRenderer>() != null)
            GetComponent<SpriteRenderer>().enabled = false;
    }
}