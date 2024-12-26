using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musouloom_Behaviour : MonoBehaviour
{
    Animator animator;
    Collider2D col;
    [SerializeField] float jumpForce = 3.0f;
    [SerializeField]


    void Start()
    {
        animator=GetComponentInChildren<Animator>();
        col= col.GetComponent<Collider2D>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            //JumpOutFromEarth();
            Debug.Log("OK");
        }

    }

    void idle()
    {
        animator.SetBool("idle", true);
    }

    void hide()
    {
        animator.SetBool("idle", false);
    }

    void JumpOutFromEarth()
    {
        animator.SetBool("idle", false);
        animator.SetTrigger("jumpOut");

    }

    void Move()
    {
        animator.SetTrigger("run");
    }

    void Explosion()
    {
        animator.SetTrigger("explosion");
    }
}
