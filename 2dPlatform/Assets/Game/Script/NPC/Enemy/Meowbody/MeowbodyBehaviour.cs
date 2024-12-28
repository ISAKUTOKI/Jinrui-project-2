using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeowbodyBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FlipLeft()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }
    void FlipRight()
    {
        transform.localScale = new Vector3(-1, 1, 1);
    }
}
