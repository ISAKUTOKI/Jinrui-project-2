using System.Collections;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public static PlayerBehaviour instance;

    public PlayerHealthBehaviour health { get;private set;}
    public PlayerMove move{ get;private set;}
    public PlayerAttackBehaviour attack{ get;private set;}
    public Animator animator{ get;private set;}
    public Transform flip{ get;private set;}

    private void Awake()
    {
        instance = this;
        attack = GetComponent<PlayerAttackBehaviour>();
        move = GetComponent<PlayerMove>();
        health = GetComponent<PlayerHealthBehaviour>();
    }

    public void Init()
    {
         health.FullFill();
    }
}