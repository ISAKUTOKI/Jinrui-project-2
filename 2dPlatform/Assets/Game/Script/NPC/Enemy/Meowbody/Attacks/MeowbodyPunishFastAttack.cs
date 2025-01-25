using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeowbodyPunishFastAttack : MonoBehaviour
{
    private float checkTimer = 4.3f;
    [HideInInspector] public bool isChecking;
    // Start is called before the first frame update
    void Start()
    {
        isChecking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isChecking)
        {
            checkTimer -= Time.deltaTime;
        }

        if (checkTimer <= 0)
        {
            MeowbodyBehaviour.instance.attack.FastAttack();
            ResetCheckTimer();
        }

        if (Input.GetKeyDown(KeyCode.F11))
        {
            Debug.Log("检查计时为 " + checkTimer);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == MeowbodyBehaviour.instance.Neko)
        {
            isChecking = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == MeowbodyBehaviour.instance.Neko)
        {
            isChecking = false;
            ResetCheckTimer();
        }
    }
    public void ResetCheckTimer()
    {
        checkTimer = 4.3f;
    }


}
