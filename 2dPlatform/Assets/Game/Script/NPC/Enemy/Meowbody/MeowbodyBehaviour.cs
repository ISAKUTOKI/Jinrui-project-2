using Assets.Game.Script.HUD_interface.Combat;
using UnityEngine;

public class MeowbodyBehaviour : MonoBehaviour
{
    public static MeowbodyBehaviour instance;

    public WeaponPowerSystem weaponPowerSystem { get; private set; }

    public MeowbodyAttackBehaviour attack { get; private set; }
    public MeowbodyHealthBehaviour health { get; private set; }
    public MeowbodyView MeowbodyView { get; private set; }
    public Animator animator { get; private set; }
    public GameObject view { get; private set; }


    public MeowbodyAttack1 attack1 { get; private set; }
    public MeowbodyAttack2 attack2 { get; private set; }
    public MeowbodyDiedAttack diedAttack { get; private set; }
    public MeowbodyFastAttack fastAttack { get; private set; }
    public MeowbodyPunishFastAttack punishFastAttack { get; private set; }



    public GameObject Neko;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        weaponPowerSystem = WeaponPowerSystem.instance;

        attack = GetComponent<MeowbodyAttackBehaviour>();
        health = GetComponent<MeowbodyHealthBehaviour>();

        MeowbodyView = GetComponentInChildren<MeowbodyView>();
        animator = GetComponentInChildren<Animator>();

        attack1=GetComponentInChildren<MeowbodyAttack1>();
        attack2 = GetComponentInChildren<MeowbodyAttack2>();
        diedAttack = GetComponentInChildren<MeowbodyDiedAttack>();
        fastAttack = GetComponentInChildren<MeowbodyFastAttack>();
        punishFastAttack = GetComponentInChildren<MeowbodyPunishFastAttack>();

        view = MeowbodyView.gameObject;
    }
}