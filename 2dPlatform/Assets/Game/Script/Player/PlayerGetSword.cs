using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGetSword : MonoBehaviour
{
    Animator animator;

    public bool hasGotSword = true;///默认为真，手动设置
    private bool canSetPlayerPhase = true;

    float tapTimer = 0.0f;
    float tapDuration = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        if (hasGotSword)
        {
            canSetPlayerPhase = false;
        }
        else
        {
            canSetPlayerPhase = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canSetPlayerPhase)
        {
            if (hasGotSword)
            {
                SetPlayerHaveWeapon();
                canSetPlayerPhase = false;
            }
        }
    }

    public void TryToGetSword()
    {
        animator.SetBool("tryToGetSword", true);
    }
    public void TappingJKey()
    {
        animator.SetBool("isGettingSword", true);
        tapTimer = Time.time;
        if (tapTimer - Time.time > tapDuration)
            animator.SetBool("isGettingSword", false);

    }
    public void FailToGetSword()
    {
        animator.SetBool("tryToGetSword", false);
    }

    void SetPlayerHaveWeapon()
    {
        PlayerBehaviour.instance.attack.canAttack = true;
        PlayerBehaviour.instance.deflect.canDeflect = true;
    }
}