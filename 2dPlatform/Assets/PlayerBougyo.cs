using Assets.Game.Script.HUD_interface.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBougyo : MonoBehaviour
{
    // Start is called before the first frame update

    public WeaponPowerSystem WPS;
    public float canBougyoValue = 0.1f;
    private bool canBougyo = false;
    private bool inBougyo = false;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (WPS.power >= canBougyoValue)
            canBougyo = true;
        else
            canBougyo = false;


        if (Input.GetKeyDown(KeyCode.K) && canBougyo)
        {
            PlayerBehaviour.instance.animator.SetTrigger("bougyo_start");
            Debug.Log("bougyo start");
            inBougyo = true;
        }

        if (inBougyo && canBougyo == false || Input.GetKeyDown(KeyCode.K) == false)
        {
            PlayerBehaviour.instance.animator.SetTrigger("bougyo_out");
            Debug.Log("bougyo out");
        }



    }



}
