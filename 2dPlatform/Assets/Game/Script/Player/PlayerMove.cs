using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    private float _speedX;
    public bool isMoving { get; private set; }
    private Vector3 _defaultFilpScale;
    public PlayerActionPerformDependency dependency;
    public PlayerKnockbackBehaviour knockback { get; private set; }
    void Start()
    {
        _defaultFilpScale = PlayerBehaviour.instance.flip.localScale;
        knockback = GetComponent<PlayerKnockbackBehaviour>();
    }

    void Update()
    {
        ReadInput();
    }

    /// <summary>
    /// FixedUpdate is called once per fixed interval
    /// This interval can be set by user
    /// the default value is 0.02
    /// the FixedUpdate is the Unity physics system's interval of calculating collisions
    /// </summary>
    private void FixedUpdate()
    {
        Move();
    }

    /// <summary>
    /// read the input from keyboard to set the move parameters
    /// </summary>
    void ReadInput()
    {
        if (PlayerBehaviour.instance.health.isDead ||
            PlayerBehaviour.instance.health.isWounding)
        {
            _speedX = 0;
            return;
        }

        if (PlayerBehaviour.instance.attack.isAttacking)
        {
            if (!PlayerBehaviour.instance.jump.IsJumping)
            {
                _speedX = 0;
            }
            PlayerBehaviour.instance.animator.SetBool("walk", false);
            PlayerBehaviour.instance.weaponView.SetState(PlayerWeaponView.State.hide);
            return;
        }

        _speedX = 0;
        if (Input.GetKey(KeyCode.A))
            _speedX = _speedX - 1;
        if (Input.GetKey(KeyCode.D))
            _speedX = _speedX + 1;

        if (_speedX > 0)
        {
            isMoving = true;
            FlipRight();
            if (!PlayerBehaviour.instance.jump.IsJumping)
            {
                PlayerBehaviour.instance.animator.SetBool("walk", true);
                PlayerBehaviour.instance.weaponView.SetState(PlayerWeaponView.State.run);
            }
            else if (PlayerBehaviour.instance.jump.IsJumping)
            {
                PlayerBehaviour.instance.animator.SetBool("walk", false);
                PlayerBehaviour.instance.weaponView.SetState(PlayerWeaponView.State.run);
            }
        }
        else if (_speedX < 0)
        {
            isMoving = true;
            FlipLeft();
            if (!PlayerBehaviour.instance.jump.IsJumping)
            {
                PlayerBehaviour.instance.animator.SetBool("walk", true);
                PlayerBehaviour.instance.weaponView.SetState(PlayerWeaponView.State.run);
            }
            else if (PlayerBehaviour.instance.jump.IsJumping)
            {
                PlayerBehaviour.instance.animator.SetBool("walk", false);
                PlayerBehaviour.instance.weaponView.SetState(PlayerWeaponView.State.run);
            }
        }
        else
        {
            isMoving = false;
            if (!PlayerBehaviour.instance.jump.IsJumping)
            {
                PlayerBehaviour.instance.animator.SetBool("walk", false);
                //PlayerBehaviour.instance.weaponView.SetState(PlayerWeaponView.State.idle);
            }
        }
    }

    /// <summary>
    /// Use the move parameters to move the player
    /// </summary>
    void Move()
    {
        if (_speedX != 0)
            PlayerBehaviour.instance.movePosition.AddXMovement(Vector3.right * _speedX * speed);
        else
            PlayerBehaviour.instance.movePosition.StopXMovement();
    }

    void FlipRight()
    {
        PlayerBehaviour.instance.flip.localScale = _defaultFilpScale;
    }

    void FlipLeft()
    {
        var s = _defaultFilpScale;
        s.x = -s.x;
        PlayerBehaviour.instance.flip.localScale = s;
    }
}