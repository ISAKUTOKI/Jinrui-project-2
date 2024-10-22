using System.Collections;
using UnityEngine;

public class PlayerDeflectBehaviour : MonoBehaviour
{
    private PlayerJump _jump;

    public float deflectStartTime { get; private set; }
    public ParticleSystem ps;

    public PlayerActionPerformDependency dependency;

    //弹反和防御系统
    //弹反和防御系统
    /*
设计目的
	游戏的核心机制，对抗敌人，人物的攻击比较弱，所以要通过弹反提高对敌人的伤害能力
	同时也提供了防御的能力
	
	（秘密：更像是只狼的敌人）

操作
	按下某个键（比如k），如果玩家处于idle run状态就可以弹反，攻击的最后几帧也可以
流程
	开始弹反，先播放【弹反开始】动画，然后播放【防御】动画
	【弹反开始】动画期间，松开按键，依旧会播放完【弹反开始】动画
	【防御】动画期间，松开按键，结束播放回到idle动画
	弹反成功时，会播放【弹反成功】动画，结束播放回到idle动画

判定
	时机：【弹反开始】动画期间以及【防御】动画期间的前几帧，可以进行弹反
	位置：远程攻击的伤害本体在玩家面前，近战攻击的发起者在玩家面前

资源
	弹反可以积累【防御条】，防御会消耗【防御条】，【防御条】为0时无法防御（会直接结束防御）
	攻击时会消耗【防御条】并强化攻击，攻击速度大幅度提高
	会自然缓慢减少

UI
    一把刀的进度条

*/





    private void Awake()
    {
   
    }

    private void Update()
    {
    
    }

    public void OnCheckDeflect(Transform damageSource)
    {
        //Debug.LogWarning("OnCheckCombo " + _comboOn);
        //PlayerBehaviour.instance.animator.ResetTrigger("combo");

        //PlayerBehaviour.instance.weaponView.SetState(PlayerWeaponView.State.idle);
    }

    public bool isDeflectingOrDefending
    {
        get
        {
            return false;
        }
    }

    void TryDefend()
    {
        PerformDefend();
    }

    void PerformDefend()
    {

    }

    public void OnDeflected(bool isSecondDamage)
    {

    }
}