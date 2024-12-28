using Assets.Game.Script.HUD_interface.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testPower : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(WeaponPowerSystem.instance.power);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
            Debug.Log(WeaponPowerSystem.instance.power);
    }
}
