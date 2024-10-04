using System.Collections;
using UnityEngine;

public class PlayerAnimationEventReceiver : MonoBehaviour
{
    public PlayerBehaviour player;

    private void Awake()
    {
        player = GetComponentInParent<PlayerBehaviour>();
    }
    public void OnAttacked()
    {
        //Debug.Log("PlayerAnimationEventReceiver OnAttacked");
        player.attack.OnAttacked();
    }
}