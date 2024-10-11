using com;
using DG.Tweening;
using System.Collections;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public HpBarFixedWidthBehaviour hpbar;
    public int hpMax;
    private int _hp;
    bool _dead;

    public string dieSound;
    public float deathFadeDelay;

    [HideInInspector]
    public NpcController npcController;
    [HideInInspector]
    public EnemyPatrolBehaviour patrolBehaviour;
    [HideInInspector]
    public EnemySkillBehaviour skillBehaviour;
    [HideInInspector]
    public EnemyPlayerChecker playerChecker;
    [HideInInspector]
    public Animator animator;
    public bool isBoss;
    private void Awake()
    {
        npcController = GetComponent<NpcController>();
        patrolBehaviour = GetComponent<EnemyPatrolBehaviour>();
        skillBehaviour = GetComponent<EnemySkillBehaviour>();
        playerChecker = GetComponent<EnemyPlayerChecker>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        _hp = hpMax;
        if (hpbar != null)
        {
            hpbar.Set(1, true);
            hpbar.Hide();
        }
    }

    private void Update()
    {
        DoRoutineMove();
    }

    public void TakeFatalDamage()
    {
        if (_dead)
            return;

        TakeDamage(hpMax + 1);
    }

    public void TakeDamage(int dmg)
    {
        if (_dead) return;

        Debug.Log(this.name + "TakeDamage " + dmg);

        _hp -= dmg;
        if (_hp < 0)
            _hp = 0;
        float ratio = (float)_hp / hpMax;
        if (hpbar != null)
            hpbar.Set(ratio, false);

        if (_hp <= 0)
            Die();
        else
        {
            animator.SetTrigger("jump");
            CombatSystem.instance.ShakeMid();
            CombatSystem.instance.BloodWeak();
        }
    }

    void Die()
    {
        if (_dead)
            return;

        _dead = true;
        if (hpbar != null)
            hpbar.Hide();

        SoundSystem.instance.Play(dieSound);
        npcController.SetAnimTrigger("die");
        npcController.StopMove();
        //npcController.myCollider.enabled = false;

        //CapsuleCollider2D col = npcController.myCollider as CapsuleCollider2D;
        //col.size = new Vector2(col.size.x * 0.25f, col.size.y * 0.25f);
        StartCoroutine(DieProcess());

        // GameFlowSystem.instance.ToggleBossHpBar(false);
    }

    IEnumerator DieProcess()
    {
        yield return new WaitForSeconds(deathFadeDelay);
        //npcController.myCollider.enabled = false;
        SpriteRenderer[] srs = GetComponentsInChildren<SpriteRenderer>();
        foreach (var sr in srs)
            sr.DOFade(0, 1.5f).SetDelay(Random.Range(0.6f, 1f));
        transform.DOShakeRotation(2, 15, 10);
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

    void DoRoutineMove()
    {
        if (_dead) return;
        //??
    }

    public bool IsDead { get { return _dead; } }
}