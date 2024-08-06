using System.Collections;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    public float speed;
    private NpcMovePosition _movePosition;
    private float _speedX;
    public Transform flipTransfrom;
    [HideInInspector]
    public Collider2D myCollider;
    private Animator _animator;
    public bool isMoving { get; private set; }
    void Awake()
    {
        _movePosition = GetComponent<NpcMovePosition>();
        myCollider = GetComponent<Collider2D>();
        if (_animator == null)
            _animator = GetComponentInChildren<Animator>();
    }

    public void Reinit(Animator a, Transform f)
    {
        _animator = a;
        flipTransfrom = f;
    }

    private void FixedUpdate()
    {
        Move();
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

    public void SetMove(bool rightOrLeft, bool startOrEnd)
    {
        if (startOrEnd)
        {
            if (rightOrLeft)
            {
                FlipRight();
                _speedX = 1;
                SetAnimBool("walk", true);
            }
            else
            {
                FlipLeft();
                _speedX = -1;
                SetAnimBool("walk", true);
            }
        }
        else
        {
            _speedX = 0;
            SetAnimBool("walk", false);
        }

    }


    public void FlipRight()
    {
        flipTransfrom.localScale = new Vector3(1, 1, 1);
    }

    public void FlipLeft()
    {
        flipTransfrom.localScale = new Vector3(-1, 1, 1);
    }

    public void SetAnimTrigger(string key, bool isReset = false)
    {
        if (isReset)
        {
            _animator.ResetTrigger(key);
            return;
        }

        _animator.SetTrigger(key);
    }

    public void SetAnimBool(string key, bool b)
    {
        _animator.SetBool(key, b);
    }
}