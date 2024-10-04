using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightJumpAttack : MonoBehaviour
{
    public Animator animator;
    public KnightGroundCheck kgc;
    public Rigidbody2D rb;
    public float jumpAttackForce = 8.0f;
    bool jumpAttacked;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (CanJumpAttack() && Input.GetKeyDown("j"))
        {
            JumpAttack();
            jumpAttacked = true;
        }
        if (kgc.isOnGround)
        {
            jumpAttacked = false;
        }

    }


    bool CanJumpAttack()
    {
        //在空中
        //没有空中攻击过
        if (kgc.isOnGround)
            return false;
        if(jumpAttacked)
            return false;

        return true;
    }


    void JumpAttack()
    {
        animator.SetTrigger("jumpAttack");
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpAttackForce, ForceMode2D.Impulse);
        //Debug.Log("跳跃攻击");
    }

}
