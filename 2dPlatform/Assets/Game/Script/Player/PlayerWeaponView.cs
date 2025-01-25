using System.Collections;
using UnityEngine;

public class PlayerWeaponView : MonoBehaviour
{
    [SerializeField] Transform weaponParent;
    [SerializeField] Transform weaponFollow;
    [SerializeField] Transform weaponFlip;
    [SerializeField] Sprite weaponIdle;
    [SerializeField] Sprite weaponRun;
    [SerializeField] SpriteRenderer sr;

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float moveSpeedDistanceFactor = 0.2f;

    public State state { get; private set; }
    public enum State
    {
        idle,
        run,
        hide,
    }

    public void SmartSetState()
    {
        var res = State.idle;
        if (PlayerBehaviour.instance.defend.isDefending)
            res = State.hide;
        if (PlayerBehaviour.instance.defend.isInDefendEnd)
            res = State.hide;
        if (PlayerBehaviour.instance.attack.isAttacking)
            res = State.hide;
        if (PlayerBehaviour.instance.move.isMoving)
            res = State.run;
        PlayerBehaviour.instance.weaponView.SetState(res);
    }

    public void SetState(State s)
    {
        if (state == s)
        {
            return;
        }

        Debug.Log("PlayerWeaponView SetState " + s);
        state = s;
        switch (state)
        {
            case State.idle:
                //Debug.Log("当前显示图片 " + state);
                sr.sprite = weaponIdle;
                sr.enabled = true;
                break;
            case State.run:
                //Debug.Log("当前显示图片 " + state);
                sr.sprite = weaponRun;
                sr.enabled = true;
                break;
            case State.hide:
                //Debug.Log("当前不显示图片");
                sr.enabled = false;
                break;
        }
    }

    void LateUpdate()
    {

        if (!PlayerBehaviour.instance.getSword.hasGotSword)
        {
            SetState(State.hide);
            return;
        }
        //Debug.Log("正在状态检测");
        //if (state == State.hide)
        //{
        //    Debug.Log("当前为隐藏状态");
        //    return;
        //}

        //Debug.Log("当前为显示状态");

        Vector3 p0 = weaponFollow.position;
        Vector3 p1 = weaponParent.position;

        if (p0 != p1)
        {
            weaponFlip.localScale = new Vector3(p0.x < p1.x ? 1 : -1, 1, 1);
            var dir = p1 - p0;
            var distance = dir.magnitude;
            var finalSpeed = moveSpeed;

            if (distance > moveSpeed * moveSpeedDistanceFactor)
                finalSpeed *= (distance / (moveSpeed * moveSpeedDistanceFactor));

            var deltaMove = finalSpeed * Time.deltaTime;

            Vector3 move = dir.normalized * deltaMove;
            if (distance < deltaMove)
            {
                weaponFollow.position = p0;
                return;
            }

            weaponFollow.position += move;
        }
        else
        {
            weaponFlip.localScale = new Vector3(PlayerBehaviour.instance.flip.localScale.x > 0 ? 1 : -1, 1, 1);
        }


    }
}