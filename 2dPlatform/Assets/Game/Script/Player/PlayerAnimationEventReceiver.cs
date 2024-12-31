using Assets.Game.Script.HUD_interface.Combat;
using System.Collections;
using UnityEngine;

public class PlayerAnimationEventReceiver : MonoBehaviour
{
    public void OnCheckCombo()
    {
        PlayerBehaviour.instance.attack.OnCheckCombo();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="n">这段攻击的第几下判定，影响激活哪个碰撞盒</param>
    public void OnAttacked(int n)
    {
        PlayerBehaviour.instance.attack.CheckDamageCollision(n);
    }
    public void StartIdleAnime()
    {
        PlayerBehaviour.instance.weaponView.SetState(PlayerWeaponView.State.idle);
    }

    public void StartRunAnime()
    {
        PlayerBehaviour.instance.weaponView.SetState(PlayerWeaponView.State.run);
    }

    public void ResetScene()
    {
        StartCoroutine(PlayerBehaviour.instance.health.SlowMoAndReload());
    }
}