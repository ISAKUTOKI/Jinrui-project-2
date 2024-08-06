using System.Collections;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public static PlayerBehaviour instance;

    [HideInInspector]
    public PlayerHealthBehaviour health;
    [HideInInspector]
    public PlayerMove move;
    [HideInInspector]
    public PlayerAttackBehaviour attack;
    [HideInInspector]
    public Animator animator;
    public Animator animator_warrier;
    public Animator animator_nonWarrier;

    [HideInInspector]
    public Transform flip;
    public Transform flip_warrier;
    public Transform flip_nonWarrier;
    NpcController _npcController;
    public bool isWarrierState;

    private void Awake()
    {
        instance = this;
        attack = GetComponent<PlayerAttackBehaviour>();
        move = GetComponent<PlayerMove>();
        health = GetComponent<PlayerHealthBehaviour>();
        _npcController = GetComponent<NpcController>();

        ToggleWarrierState(false);
    }

    public void ToggleWarrierState(bool b)
    {
        isWarrierState = b;
        animator = b ? animator_warrier : animator_nonWarrier;
        flip = b ? flip_warrier : flip_nonWarrier;
        flip_warrier.gameObject.SetActive(b);
        flip_nonWarrier.gameObject.SetActive(!b);

        _npcController.Reinit(animator, flip);
        health.FullFill(b);
    }
}