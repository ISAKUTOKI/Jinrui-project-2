﻿using UnityEngine;
using System.Collections;

public class PlayerJump : MonoBehaviour
{
    public float jumpPower = 10;
    public PlayerGroundDetecter groundDetecter;
    bool _isFloating { get { return !groundDetecter.isGrounded; } }

    void Awake()
    {
    }

    void Update()
    {
        ReadInput();
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
        if (Input.GetKeyDown(KeyCode.W))
            TryJump();
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
        //PlayerBehaviour.instance.animator.SetTrigger("jump");
        PlayerBehaviour.instance.movePosition.rb.AddForce(new Vector2(0, jumpPower));
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
        PlayerBehaviour.instance.weaponView.SetState(PlayerWeaponView.State.idle);
    }
}