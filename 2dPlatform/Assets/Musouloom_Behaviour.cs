using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musouloom_Behaviour : MonoBehaviour
{
    Animator animator;
    Object musouloomObject;
    //[SerializeField]

    public GameObject alarmSpotL;
    public GameObject alarmSpotR;
    public GameObject jumpSpotL;
    public GameObject jumpSpotR;
    public GameObject Neko;
    public Collider2D NekoCollider;


    float moveSpeed = 2.0f;
    public bool readyToSearch = false;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            animator.SetBool("idle", true);
            animator.SetTrigger("jumpOut");
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
    public void Explosion()
    {
        animator.SetTrigger("explosion");
    }
    public void Search()
    {
        Debug.Log("Ready To Search");
        Vector2 playerPosition = Neko.transform.position;
        float directionToPlayerX = playerPosition.x - transform.position.x;
        Vector2 moveDirection = new Vector2(directionToPlayerX * moveSpeed * Time.deltaTime, 0);
        transform.position = (Vector2)transform.position + moveDirection;

    }
    public void Die()
    {
        Debug.Log("Die");
        //Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        bool canAlarm = true;
        bool canJump = true;

        if (canAlarm && collision.gameObject == alarmSpotL || canAlarm && collision.gameObject == alarmSpotR)
        {
            Debug.Log("alarm enter");
            idle();
        }

        if (canJump && collision.gameObject == jumpSpotL || canJump && collision.gameObject == jumpSpotR)
        {
            Debug.Log("jump");
            JumpOutFromEarth();
            canAlarm = false;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == alarmSpotL || collision.gameObject == alarmSpotR)
        {
            Debug.Log("alarm exit");
            hide();
        }
    }
}
