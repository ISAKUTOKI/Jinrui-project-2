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
    private Vector3 _startScale;
    public bool isMoving { get; private set; }
    public bool defaultFacingRight;

    void Awake()
    {
        _movePosition = GetComponent<NpcMovePosition>();
        myCollider = GetComponent<Collider2D>();
        if (_animator == null)
            _animator = GetComponentInChildren<Animator>();

        _startScale = flipTransfrom.localScale;
        if (!defaultFacingRight)
            _startScale.x = -_startScale.x;
    }

    public void Reinit(Animator a, Transform f)
    {
        _animator = a;
        flipTransfrom = f;
    }

    private void FixedUpdate()
    {
        if (_movePosition != null)
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

    public void StopMove()
    {
        _speedX = 0;
        SetAnimBool("walk", false);
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
            StopMove();
        }

    }

    public void FlipRight()
    {
        flipTransfrom.localScale = _startScale;
    }

    public void FlipLeft()
    {
        flipTransfrom.localScale = new Vector3(-_startScale.x, _startScale.y, _startScale.z);
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