using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeowbodyPunishFastAttack : MonoBehaviour
{
    public MeowbodyGetComponent component;
    float checkTimer;
    float checkDuration = 4.3f;
    [HideInInspector] public bool canCheck;
    // Start is called before the first frame update
    void Start()
    {
        canCheck = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canCheck)
        {
            checkTimer += Time.deltaTime;
        }

        if (checkTimer > checkDuration)
        {
            component.attackSystem.FastAttack();
            checkTimer = 0;
        }

        if (Input.GetKeyDown(KeyCode.F11))
        {
            Debug.Log("检查计时为 " + checkTimer);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == component.Neko)
        {
            canCheck = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == component.Neko)
        {
            canCheck = false;
            checkTimer = 0;
        }
    }


}
