using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightJump : MonoBehaviour
{
    public KnightGroundCheck kgc;
    public KnightAttack ka;
    public Rigidbody2D rb;
    public Animator animator;
    public float jumpForce = 10f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (CanJump() && Input.GetKeyDown("k"))
        {
            Jump();
        }
    }

    bool CanJump()
    {
        //在地面
        if (!kgc.isOnGround)
            return false;
        //不在受击
        if (IsHurting())
            return false;
        else
            return true;
    }

    bool IsHurting()
    {
        if (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Hurt Animation")
        {
            return true;
        }
        return false;
    }

    private void Jump()
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        animator.SetTrigger("jump");
    }
}
