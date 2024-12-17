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
        //Debug.Log(WPS.power);
    }

    // Update is called once per frame
    void Update()
    {
        //判断能否防御
        CanBougyo();

        //防御输入
        BougyoCheck();
    }

    void CanBougyo()
    {
        if (WPS.power > canBougyoValue)
        {
            canBougyo = true;
        }
        else
        {
            canBougyo = false;
        }
    }

    void BougyoCheck()
    {
        //按下K并且可以防御并且不在防御那就进入防御
        if (Input.GetKeyDown(KeyCode.K) && canBougyo && inBougyo == false)
        {
            PlayerBehaviour.instance.animator.SetTrigger("bougyo_start");
            //debug开始防御
            //Debug.Log("bougyo start");
            //防御中
            inBougyo = true;
        }

        //在防御中并且松开了k键就停止防御
        if (inBougyo == true && Input.GetKeyDown(KeyCode.K) == false)
        {
            PlayerBehaviour.instance.animator.SetTrigger("bougyo_out");
            //Debug.Log("检测");
            inBougyo = false;
        }

    }
}
