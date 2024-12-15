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
        Debug.Log("canDebugLog");
        Debug.Log(WPS.power);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        Debug.Log(WPS.power);

        if (WPS.power > canBougyoValue)
        {
            canBougyo = true;
            //Debug.Log("canbougyo");
        }
        else
        {
            canBougyo = false;
            //Debug.Log("canNotBougyo");
        }



        //按下K并且可以防御并且不在防御那就进入防御
        if (Input.GetKeyDown(KeyCode.K) && canBougyo && inBougyo == false)
        {
            //防御动画
            PlayerBehaviour.instance.animator.SetTrigger("bougyo_start");
            //debug开始防御
            //Debug.Log("bougyo start");
            //防御中
            inBougyo = true;
        }

        //在防御中
        if (inBougyo == true && Input.GetKeyDown(KeyCode.K) == false)
        {
            PlayerBehaviour.instance.animator.SetTrigger("bougyo_out");
            //Debug.Log("检测");
        }

    }


}
