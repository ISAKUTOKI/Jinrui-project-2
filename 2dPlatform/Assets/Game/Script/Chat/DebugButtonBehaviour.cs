using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugButtonBehaviour : MonoBehaviour
{
    [SerializeField] bool isInTest;
    [SerializeField] GameObject panelDebug;
    // Start is called before the first frame update
    void Start()
    {
        if (isInTest)
            panelDebug.SetActive(true);
        else
            panelDebug.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }


}
