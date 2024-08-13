using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Turn3DPage : MonoBehaviour
{
    [SerializeField] Transform _panel1Trans;
    [SerializeField] Transform _panel2Trans;


    void Start()
    {
        _panel2Trans.localEulerAngles = new Vector3(0, -100, 0);
        _panel1Trans.localEulerAngles = new Vector3(0, -100, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            Debug.Log("panel 1 show");
            _panel1Trans.DOLocalRotate(new Vector3(0, 0, 0), 0.5f);
        }
        if (Input.GetKeyDown("2"))
        {
            Debug.Log("panel 2 show");
            _panel2Trans.DOLocalRotate(new Vector3(0, 0, 0), 0.5f);
        }
        if (Input.GetKeyDown("3"))
        {
            Debug.Log("panel 1 hide");
            _panel1Trans.DOLocalRotate(new Vector3(0, -100, 0), 0.5f);
        }
        if (Input.GetKeyDown("4"))
        {
            Debug.Log("panel 2 hide");
            _panel2Trans.DOLocalRotate(new Vector3(0, -100, 0), 0.5f);
        }
    }
}
