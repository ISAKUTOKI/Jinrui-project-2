using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightMove : MonoBehaviour
{

    public Transform view;
    public Animator animator;
    public KnightGroundCheck kgc;
    public KnightAttack ka;
    public float speed;
    public float jumpForce = 10.0f;
    bool isHurt = false;
    float hurtInterval = 0.25f;
    float hurtTime;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Hurt()
    {
        animator.SetTrigger("hurt");
        isHurt = true;
        hurtTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {

        var h = Input.GetAxis("Horizontal");

        if (Time.time - hurtTime >= hurtInterval)
        {
            isHurt = false;
        }

        if (CanMove())
        {
            transform.position += Vector3.right * h * speed * Time.deltaTime;

            if (h > 0)
            {
                FlipRight();
                animator.SetBool("walk", true);
            }
            else if (h < 0)
            {
                FlipLeft();
                animator.SetBool("walk", true);
            }
            else
            {
                animator.SetBool("walk", false);
            }

        }
        else
        {
            animator.SetBool("walk", false);
        }
    }

    bool CanMove()
    {
        //没有受伤
        //没有攻击中
        if(isHurt)
            return false;
        if (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Attack Animation")
            return false;   

        return true;
    }

    void FlipRight()
    {
        view.localScale = new Vector3(1, 1, 1);
    }

    void FlipLeft()
    {
        view.localScale = new Vector3(-1, 1, 1);
    }


}
