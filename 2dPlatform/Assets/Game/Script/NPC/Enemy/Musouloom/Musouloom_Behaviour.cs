using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musouloom_Behaviour : MonoBehaviour
{
    Animator animator;
    Object musouloomObject;
    public GameObject Neko;
    float moveSpeed = 2.5f;
    [HideInInspector] public bool canAlarm;
    [HideInInspector] public bool canJumpOutFromEarth;
    [HideInInspector] public bool canSearch = false;


    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        canAlarm = true;
        canJumpOutFromEarth = true;
    }
    void Update()
    {
        Search();
    }

    public void idle()
    {
        animator.SetBool("idle", true);
    }
    public void hide()
    {
        animator.SetBool("idle", false);
    }
    public void JumpOutFromEarth()
    {
        animator.SetTrigger("jumpOut");
        canJumpOutFromEarth = false;
        StartCoroutine(MoveUpDownFromEarth(0.2f, 0.15f, -0.04f, 0.1f));
    }
    IEnumerator MoveUpDownFromEarth(float upAmount, float upTime, float downAmount, float downTime)
    {
        float upElapsedTime = 0;
        float downElapsedTime = 0;
        Vector3 startPosition = transform.position;
        Vector3 targetPositionUp = startPosition + new Vector3(0, upAmount, 0);
        Vector3 targetPositionDown = startPosition + new Vector3(0, -downAmount, 0);

        while (upElapsedTime < upTime)
        {
            transform.position = Vector3.Lerp(startPosition, targetPositionUp, (upElapsedTime / upTime));
            upElapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPositionUp;

        yield return new WaitForSeconds(0.05f);

        while (downElapsedTime < downTime)
        {
            transform.position = Vector3.Lerp(targetPositionUp, targetPositionDown, (downElapsedTime / downTime));
            downElapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPositionDown;
    }
    public void Search()
    {
        if (canSearch)
        {
            Vector3 playerPosition = Neko.transform.position;
            float directionToPlayerX = playerPosition.x - transform.position.x;
            Vector3 moveDirection = new Vector3(moveSpeed * Time.deltaTime, 0, 0);
            if (directionToPlayerX != 0)
            {
                moveDirection.x *= directionToPlayerX / Mathf.Abs(directionToPlayerX);
            }
            transform.position = transform.position + moveDirection;

            if (directionToPlayerX > 0)
                FlipRight();
            if (directionToPlayerX < 0)
                FlipLeft();
        }
    }
    public void Explosion()
    {
        animator.SetTrigger("explosion");
        StartCoroutine(ExplosionMoveUp(0.6f, 0.1f));
        //Debug.Log("OK");
    }
    IEnumerator ExplosionMoveUp(float distance, float time)
    {
        float elapsedTime = 0;
        Vector3 startPosition = transform.position;
        while (elapsedTime < time)
        {
            transform.position = Vector3.Lerp(startPosition, startPosition + new Vector3(0, distance, 0), (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = startPosition + new Vector3(0, distance, 0); // 确保最终位置正确
    }
    public void ExplosionHurtPlayer()
    {


    }

    public void Die()
    {
        //Debug.Log("Die");
        Destroy(gameObject);
    }

    void FlipLeft()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }

    void FlipRight()
    {
        transform.localScale = new Vector3(-1, 1, 1);
    }


}
