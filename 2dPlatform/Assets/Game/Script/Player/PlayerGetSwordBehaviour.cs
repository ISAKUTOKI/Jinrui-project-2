using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PlayerGetSwordBehaviour : MonoBehaviour
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
            //Debug.Log("有剑");
            canSetPlayerPhase = false;
            SetPlayerHaveWeapon();
        }
        else
        {
            //Debug.Log("没剑");
            //PlayerBehaviour.instance.weaponView.SetState(PlayerWeaponView.State.hide);
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
        PlayerBehaviour.instance.weaponView.SetState(PlayerWeaponView.State.idle);
    }
}