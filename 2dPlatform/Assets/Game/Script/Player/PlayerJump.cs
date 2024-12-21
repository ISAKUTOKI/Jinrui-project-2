using UnityEngine;
using System.Collections;

public class PlayerJump : MonoBehaviour
{
    public float jumpPower = 1000;
    public PlayerGroundDetecter groundDetecter;
    bool _isFloating { get { return !groundDetecter.isGrounded; } }

    void Awake()
    {
    }

    void Update()
    {
        ReadInput();

        //if (Input.GetKeyDown(KeyCode.F12))
        //{
        //    Debug.Log("");
        //    Debug.Log("起跳时间是 " + jumpUpTime);
        //    Debug.Log("当前时间是 " + Time.time);
        //    Debug.Log("时间差为 " + (Time.time - jumpUpTime));
        //    if (Time.time - jumpUpTime >= timeToDrop)
        //        Debug.Log("准备下落");
        //    else
        //        Debug.Log("还不能下落");
        //}
        //if (Time.time - jumpUpTime >= timeToDrop && canDrop)
        //{
        //    Drop();
        //    //Debug.Log("准备下落");
        //}
    }

    public bool IsJumping { get { return _isFloating; } }

    void ReadInput()
    {
        if (com.GameTime.timeScale == 0)
            return;
        //    if (ChatSystem.instance.IsChating())
        //       return;

        if (PlayerBehaviour.instance.attack.isAttacking)
            return;
        if (PlayerBehaviour.instance.health.isDead)
            return;
        if (PlayerBehaviour.instance.health.isWounding)
            return;
        if (Input.GetKeyDown(KeyCode.W))
        {
            TryJump();
        }
    }

    void TryJump()
    {
        if (canNotJump)
            return;

        DoJump();
    }


    bool canNotJump { get { return _isFloating || PlayerBehaviour.instance.attack.isAttacking || PlayerBehaviour.instance.health.isDead; } }

    void DoJump()
    {
        //_speedY = jumpPower;
        PlayerBehaviour.instance.animator.SetBool("walk", false);
        PlayerBehaviour.instance.weaponView.SetState(PlayerWeaponView.State.idle);
        PlayerBehaviour.instance.movePosition.rb.AddForce(new Vector2(0, jumpPower));
        PlayerBehaviour.instance.animator.SetBool("jump", true);
    }

    public void OnGrounded()
    {
        var v = PlayerBehaviour.instance.movePosition.rb.velocity;
        if (v.y > 0)
            return;
        v.y = 0;
        PlayerBehaviour.instance.movePosition.rb.velocity = v;
        PlayerBehaviour.instance.movePosition.StopXMovement();
        PlayerBehaviour.instance.animator.SetBool("walk", false);
        PlayerBehaviour.instance.animator.SetBool("jump", false);
        PlayerBehaviour.instance.weaponView.SetState(PlayerWeaponView.State.idle);
        //Debug.Log("jumpUpTime清零");
    }

    //void JumpDropSwitch()
    //{
    //    PlayerBehaviour.instance.animator.SetBool("jump", true);
    //    PlayerBehaviour.instance.animator.SetBool("jump", false);

    //    PlayerBehaviour.instance.animator.SetBool("drop", true);
    //    PlayerBehaviour.instance.animator.SetBool("drop", false);
    //}

}