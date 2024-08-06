using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class EnemyPatrolBehaviour : MonoBehaviour
{
    //巡逻
    //设计原则
    //敌人在游戏中会进行左右巡逻

    //巡逻共有4个状态，【GoRight GoLeft StopFacingLeft StopFacingRight】
    //巡逻的模式固定为：【向右走，脸朝右停下，向左走，脸朝左停下】
    //设置左右两个【patrol point】，和左右两个【safe point】

    //当敌人没有发现玩家时走到左右两个patrol point就意味着会进入停下状态，并在接下来转身
    //当敌人发现玩家后，敌人不会在patrol point停下，而是会继续追击
    //【敌人发现玩家的条件是：玩家对敌人造成伤害，或者玩家在敌人的前方不远处】
    //当敌人走到safe point一定会进入停下状态，并在接下来转身，不管有没有发现玩家。这是防止敌人去到不合理的区域
    //敌人会使用普通攻击或者技能攻击在攻击范围附近的玩家，这些行为会让敌人的移动暂时停下，但是不影响巡逻的逻辑

    //example
    //case 1: 无玩家干扰
    //敌人会在左右两个patrol point之间来回巡逻
    //case 2: 玩家在背后攻击敌人
    //敌人会立刻停下，并在过一小段时间后转身
    //case 3: 玩家在敌人面前逃跑
    //敌人会一直追击，直到到达一端的safe point
    //case 4: 玩家跳到敌人后面
    //敌人如果已经超过了patrol point，会停下，否则继续巡逻到下一个patrol point

    EnemyBehaviour _enemy;
    Rigidbody2D _rb2D;
    public Transform patrolPoint_Left;
    public Transform patrolPoint_Right;
    public Transform safePoint_Left;
    public Transform safePoint_Right;
    public float stopDuration = 0.5f;
    float _stopTimer;
    public float speed;


    public enum PatrolState
    {
        GoRight,
        GoLeft,
        StopFacingLeft,
        StopFacingRight,
    }
    public PatrolState state = PatrolState.GoRight;

    private void Awake()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        _enemy = GetComponent<EnemyBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_enemy.IsDead)
            return;
        if (_enemy.skillBehaviour.isCasting)
            return;

        CheckState();
    }

    private void FixedUpdate()
    {
        if (_enemy.IsDead)
            return;
        if (_enemy.skillBehaviour.isCasting)
            return;

        switch (state)
        {
            case PatrolState.GoRight:
                _enemy.animator.SetBool("walk", true);
                _rb2D.MovePosition((Vector2)transform.position + Vector2.right * speed * Time.fixedDeltaTime);
                break;

            case PatrolState.GoLeft:
                _enemy.animator.SetBool("walk", true);
                _rb2D.MovePosition((Vector2)transform.position + Vector2.left * speed * Time.fixedDeltaTime);
                break;
        }
    }

    void CheckState()
    {
        var alerted = _enemy.playerChecker.FoundPlayer();
        var x = transform.position.x;
        var pp_l = patrolPoint_Left.position.x;
        var pp_r = patrolPoint_Right.position.x;
        var sp_l = safePoint_Left.position.x;
        var sp_r = safePoint_Right.position.x;
        //Debug.Log("x " + x);
        //Debug.Log("pp_l " + pp_l);
        //Debug.Log("pp_r " + pp_r);
        //Debug.Log("sp_l " + sp_l);
        //Debug.Log("sp_r " + sp_r);
        if (x > sp_r)
        {
            SetState(PatrolState.GoLeft);
            return;
        }
        if (x < sp_l)
        {
            SetState(PatrolState.GoRight);
            return;
        }

        if (!alerted)
        {
            if (state == PatrolState.GoLeft || state == PatrolState.GoRight)
            {
                if (state == PatrolState.GoRight && x > pp_r)
                {
                    SetState(PatrolState.StopFacingRight);
                    return;
                }
                if (state == PatrolState.GoLeft && x < pp_l)
                {
                    SetState(PatrolState.StopFacingLeft);
                    return;
                }
            }
            else
            {
                _stopTimer -= Time.deltaTime;
                if (_stopTimer < 0)
                {
                    if (state == PatrolState.StopFacingLeft)
                    {
                        SetState(PatrolState.GoRight);
                        return;
                    }
                    if (state == PatrolState.StopFacingRight)
                    {
                        SetState(PatrolState.GoLeft);
                        return;
                    }
                }
            }
            return;
        }

        //alerted
        if (state == PatrolState.StopFacingLeft)
        {
            SetState(PatrolState.GoLeft);
            return;
        }
        if (state == PatrolState.StopFacingRight)
        {
            SetState(PatrolState.GoRight);
            return;
        }
        if (_enemy.playerChecker.FoundPlayer() && !_enemy.playerChecker.PlayerInRawSight())
        {
            if (facingRight)
                SetState(PatrolState.GoLeft);
            else
                SetState(PatrolState.GoRight);
        }
    }

    void SetState(PatrolState newState)
    {
        //Debug.Log("newState " + newState);
        var flipTrans = _enemy.npcController.flipTransfrom;
        state = newState;
        switch (state)
        {
            case PatrolState.GoRight:
                _enemy.animator.SetBool("walk", true);
                flipTrans.localScale = new Vector3(1, 1, 1);
                _stopTimer = 0;
                break;
            case PatrolState.GoLeft:
                _enemy.animator.SetBool("walk", true);
                flipTrans.localScale = new Vector3(-1, 1, 1);
                _stopTimer = 0;
                break;
            case PatrolState.StopFacingLeft:
                _enemy.animator.SetBool("walk", false);
                flipTrans.localScale = new Vector3(-1, 1, 1);
                _stopTimer = stopDuration;
                break;
            case PatrolState.StopFacingRight:
                _enemy.animator.SetBool("walk", false);
                flipTrans.localScale = new Vector3(1, 1, 1);
                _stopTimer = stopDuration;
                break;
        }
    }

    public bool facingRight
    {
        get
        {
            var flipTrans = _enemy.npcController.flipTransfrom;
            return flipTrans.localScale.x > 0;
        }
    }
}