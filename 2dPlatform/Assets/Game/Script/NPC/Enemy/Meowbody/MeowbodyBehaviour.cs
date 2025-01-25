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
    public MeowbodyPunishFastAttack punishFastAttack { get; private set; }
    public GameObject view { get; private set; }


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
        punishFastAttack = GetComponentInChildren<MeowbodyPunishFastAttack>();

        view = MeowbodyView.gameObject;
    }
}