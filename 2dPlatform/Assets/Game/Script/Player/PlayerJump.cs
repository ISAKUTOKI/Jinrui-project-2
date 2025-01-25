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


    bool canNotJump { get { return _isFloating || PlayerBehaviour.instance.defend.isInNoMoveState || PlayerBehaviour.instance.attack.isAttacking || PlayerBehaviour.instance.health.isDead; } }

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

        PlayerBehaviour.instance.animator.SetBool("jump", false);
        //Debug.Log("highFall " + v.y);
        //var highFall = false;
        //if (v.y < -12)//一般跳跃下落速度-6.624929，地震-1.95
        //{
        //    highFall = true;//掉落伤害
        //}
        //v.y = 0;
        //PlayerBehaviour.instance.movePosition.rb.velocity = v;

        //if (highFall)
        //{
        //    PlayerBehaviour.instance.movePosition.StopXMovement();
        //    PlayerBehaviour.instance.animator.SetBool("walk", false);
        //    PlayerBehaviour.instance.animator.SetBool("jump", false);
        //    PlayerBehaviour.instance.weaponView.SetState(PlayerWeaponView.State.idle);
        //    PlayerBehaviour.instance.OnHit(null);//掉落伤害 打断防御
        //}

    }

}