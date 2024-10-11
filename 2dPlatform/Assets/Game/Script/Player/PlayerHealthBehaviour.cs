using Assets.Game.Script.GameFlow;
using com;
using DG.Tweening;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerHealthBehaviour : MonoBehaviour
{
    public HpHalfHeartsBehaviour hpView;
    public int hpMax;

    private int _hpMax;
    private int _hp;
    bool _dead;

    public string dieSound;
    public float deathFadeDelay;

    private void Start()
    {
        FullFill();
    }
    public void FullFill()
    {
        _hpMax = hpMax;
        hpView.FullFillAll();
        _hp = _hpMax;
    }

    private void Update()
    {
        DoRoutineMove();
    }

    public void TakeDamage(int dmg)
    {
        if (_dead) return;

        //Debug.Log(this.name + "TakeDamage " + dmg);
        _hp -= dmg;
        if (_hp < 0)
            _hp = 0;

        //float ratio = (float)_hp / _hpMax;
        hpView.SetHp(_hp, true);
        if (_hp <= 0)
        {
            CombatSystem.instance.BloodStrong();
            Die();
        }
        else
        {
            CombatSystem.instance.BloodWeak();
            ChatBoxSystem.instance.IWantHurt();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Kill")
        {
            this.Die(true);
        }
    }

    public void Die(bool fromFall = false)
    {
        if (_dead) return;

        _dead = true;
        //ReviveSystem.instance.QueueDie(fromFall);
        SoundSystem.instance.Play(dieSound);

        if (!fromFall)
            PlayerBehaviour.instance.animator.SetTrigger("die");

        SpriteRenderer[] srs = GetComponentsInChildren<SpriteRenderer>();
        foreach (var sr in srs)
        {
            sr.DOFade(0, 3).SetDelay(deathFadeDelay + Random.Range(1, 3f));
        }
    }

    void DoRoutineMove()
    {
        if (_dead) return;


    }

    public bool isDead
    {
        get { return _dead; }
    }
}