using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    private PlayerMovePosition _movePosition;
    private float _speedX;
    private PlayerJump _jump;
    private PlayerAttackBehaviour _attack;
    private PlayerHealthBehaviour _health;
    public bool isMoving { get; private set; }

    void Start()
    {
        _movePosition = GetComponent<PlayerMovePosition>();
        _jump = GetComponent<PlayerJump>();
        _attack = GetComponent<PlayerAttackBehaviour>();
        _health = GetComponent<PlayerHealthBehaviour>();
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
        if (_health.isDead)
            return;

        if (_attack.isAttacking)
        {
            if (!_jump.IsJumping)
            {
                _speedX = 0;
            }
            PlayerBehaviour.instance.animator.SetBool("walk", false);
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
            if (!_jump.IsJumping)
                PlayerBehaviour.instance.animator.SetBool("walk", true);
            FlipRight();
        }
        else if (_speedX < 0)
        {
            isMoving = true;
            FlipLeft();
            if (!_jump.IsJumping)
                PlayerBehaviour.instance.animator.SetBool("walk", true);
        }
        else
        {
            isMoving = false;
            if (!_jump.IsJumping)
                PlayerBehaviour.instance.animator.SetBool("walk", false);
        }
    }

    /// <summary>
    /// Use the move parameters to move the player
    /// </summary>
    void Move()
    {
        if (_speedX != 0)
            _movePosition.AddXMovement(Vector3.right * _speedX * speed);
        else
            _movePosition.StopXMovement();
    }

    void FlipRight()
    {
        PlayerBehaviour.instance.flip.localScale = new Vector3(1, 1, 1);
    }

    void FlipLeft()
    {
        PlayerBehaviour.instance.flip.localScale = new Vector3(-1, 1, 1);
    }
}