using System.Collections;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public static PlayerBehaviour instance;

    public PlayerHealthBehaviour health { get; private set; }
    public PlayerMove move { get; private set; }
    public PlayerAttackBehaviour attack { get; private set; }

    public PlayerJump jump { get; private set; }

    public PlayerDeflectBehaviour defend { get; private set; }

    public PlayerDie die { get; private set; }

    public Animator animator { get; private set; }

    public PlayerMovePosition movePosition { get; private set; }

    public PlayerWeaponView weaponView { get; private set; }

    public Transform flip;

    private void Awake()
    {
        instance = this;

        animator = GetComponentInChildren<Animator>();
        movePosition = GetComponent<PlayerMovePosition>();
        attack = GetComponent<PlayerAttackBehaviour>();
        jump = GetComponent<PlayerJump>();
        move = GetComponent<PlayerMove>();
        health = GetComponent<PlayerHealthBehaviour>();
        weaponView = GetComponent<PlayerWeaponView>();
        defend = GetComponent<PlayerDeflectBehaviour>();

        // animController = animator.runtimeAnimatorController as AnimatorController;
    }

    public void Init()
    {
        weaponView.SetState(PlayerWeaponView.State.idle);
    }

    /// <summary>
    /// 最终的受到伤害的处理函数，接受的参数是伤害来源，可能的情况有
    /// 1 敌人身体，比如史莱姆的近战攻击，用的是整个敌人的transform判断弹反
    /// 2 飞射性物体projectile，也就是远程攻击，用的是敌人的子弹的transform判断弹反
    /// 3 敌人的身体部分，比如大型猫敌人的尾巴攻击，用的是这个攻击特定的攻击判定框的transform判断弹反
    /// </summary>
    /// <param name="hitSource"></param>
    public void OnHit(Transform hitSource = null)
    {
        Debug.Log("neko OnHit");
        var inDeflectArea = defend.deflectArea.InAreaCheck(hitSource);
        //Debug.Log("inDeflectArea " + inDeflectArea);
        if (hitSource != null && inDeflectArea)
        {
            Debug.Log("inDeflectArea");
            if (defend.isDefending)
            {
                if (defend.isDeflecting)
                {
                    defend.TriggerDeflect();
                    return;
                }

                var dx = hitSource.position.x - transform.position.x;
                bool isToRight = dx < 0;

                defend.TriggerDefend(isToRight);
                return;
            }
        }

        health.TakeDamage(1);
    }

    private Animator anim;
    private AnimatorController animController;


    //public void SetAnimatorSpeed(int layerIndex, string stateName, float speed)
    //{
    //    foreach (var state in animController.layers[layerIndex].stateMachine.states)
    //    {
    //        if (state.state.name == stateName)
    //        {
    //            state.state.speed = speed;
    //        }
    //    }
    //}
    //
    //public float GetAnimatorSpeed(int layerIndex, string stateName)
    //{
    //    foreach (var state in animController.layers[layerIndex].stateMachine.states)
    //    {
    //        if (state.state.name == stateName)
    //        {
    //            return state.state.speed;
    //        }
    //    }
    //
    //    return 0;
    //}
}