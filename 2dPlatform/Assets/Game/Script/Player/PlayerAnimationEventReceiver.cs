using System.Collections;
using UnityEngine;

public class PlayerAnimationEventReceiver : MonoBehaviour
{
    public PlayerMove playerMove;
    public PlayerAttackBehaviour playerAttack;

    public void OnAttacked()
    {
        //Debug.Log("PlayerAnimationEventReceiver OnAttacked");
        playerAttack.OnAttacked();
    }
}