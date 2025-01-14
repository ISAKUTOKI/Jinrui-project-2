using Assets.Game.Script.HUD_interface.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeowbodyGetComponent : MonoBehaviour
{
    public WeaponPowerSystem weaponPowerSystem;
    public GameObject Neko;
    public Transform NekoTransform;
    public PlayerHealthBehaviour NekoHealth;


    public Animator animator;
    public GameObject view;

    public MeowbodyAttackBehaviour attackSystem;
    public MeowbodyHealthBehaviour healthSystem;
    public MeowbodyPunishFastAttack fastAttackCheck;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
