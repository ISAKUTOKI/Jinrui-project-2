using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemySkillBehaviour : MonoBehaviour
{
    public EnemySkill[] skills;
    EnemySkill crtSkill;

    EnemyBehaviour _enemy;

    float _durationTimer;
    public Transform meleeCenter;

    private void Awake()
    {
        _enemy = GetComponent<EnemyBehaviour>();
    }

    public bool isCasting
    {
        get { return crtSkill != null; }
    }

    void Update()
    {
        if (_enemy.IsDead)
            return;

        TickSkills();

        foreach (var skl in skills)
        {
            var canUse = CanUseSkill(skl);
            if (canUse)
            {
                UseSkill(skl);
                break;
            }
        }
    }

    void UseSkill(EnemySkill skl)
    {
        //Debug.Log("UseSkill " + skl.id);
        crtSkill = skl;
        skl.cdTimer = skl.cd;
        _durationTimer = skl.duration;
        _enemy.animator.SetBool("walk", false);

        switch (skl.id)
        {
            case "smash":
                if (_enemy.playerChecker.FoundPlayer())
                {
                    _enemy.animator.SetTrigger("jump");
                    _enemy.npcController.StopMove();
                    StartCoroutine(Smash(true));
                }
                break;
            case "smash_noTarget":
                _enemy.animator.SetTrigger("jump");
                _enemy.npcController.StopMove();
                StartCoroutine(Smash(false));
                break;
            case "idle_consume_cd":
                foreach (var otherSkl in skills)
                {
                    if (otherSkl == crtSkill)
                        continue;
                    otherSkl.cdTimer = Random.Range(1f, 4f);
                }
                break;
        }

        if (skl.launchEffect != null)
            skl.launchEffect.Play();
    }

    IEnumerator Smash(bool targetPlayer)
    {
        var p1 = transform.position;
        var p2 = PlayerBehaviour.instance.transform.position;
        if (!targetPlayer)
        {
            p2 = p1 + new Vector3(_enemy.patrolBehaviour.facingRight ? 3 : -3, 0, 0);
        }
        var p3 = (p1 + p2) * 0.5f;
        p3.y += 3.4f;
        yield return new WaitForSeconds(0.3f);

        var turnTime = 0.35f;
        transform.DOMoveX(p2.x, turnTime * 2);

        transform.DOMoveY(p3.y, turnTime).SetEase(Ease.OutQuad);
        yield return new WaitForSeconds(turnTime);
        transform.DOMoveY(p2.y, turnTime).SetEase(Ease.InQuad);
        yield return new WaitForSeconds(turnTime);

        p1 = meleeCenter.position;
        p2 = PlayerBehaviour.instance.transform.position;
        Vector2 dist = p1 - p2;
        if (dist.magnitude < 1)
        {
            PlayerBehaviour.instance.OnHit(transform);
        }
        CombatSystem.instance.ShakeWeak();
    }

    void SummonFireBall(Vector3 fireballPos, float xOffset, GameObject prefab, Vector3 playerPos)
    {
        var ball = Instantiate(prefab, fireballPos + new Vector3(xOffset, 0, 0), Quaternion.identity);
        ball.transform.DOMove(playerPos + new Vector3(Random.Range(-0.8f, 0.8f), -1.0f, 0), Random.Range(1.9f, 2.2f)).
            SetEase(Ease.InCubic).SetDelay(Random.Range(0.1f, 0.4f)).OnComplete(
            () =>
            {
                var explosion = ball.transform.GetChild(0);
                explosion.SetParent(null);
                explosion.gameObject.SetActive(true);
                DealDamage(ball.transform);
                Destroy(ball.gameObject);
                Destroy(explosion.gameObject, 2);
            }
            );
    }

    IEnumerator DelayDamage(float delay, Transform trans)
    {
        yield return new WaitForSeconds(delay);
        DealDamage(trans);
    }

    void DealDamage(Transform trans)
    {
        if (crtSkill != null)
        {
            var pos = trans.position;
            var player = PlayerBehaviour.instance;
            bool inRange = false;
            var dist = player.transform.position - pos;
            dist.z = 0;
            dist.y *= 0.6f;

            switch (crtSkill.id)
            {
                case "sky fire":
                    dist.y *= 0.6f;
                    inRange = dist.magnitude < 1.42f;
                    break;

                case "spike":
                    dist.y *= 0.8f;
                    dist.x *= 0.9f;
                    inRange = dist.magnitude < 1.2f;
                    break;

                case "melee minion":
                    dist.y *= 0.6f;
                    inRange = dist.magnitude < 1.3f;
                    break;

                case "melee":
                    dist.y *= 0.6f;
                    inRange = dist.magnitude < 1.35f;
                    break;
            }

            //Debug.Log(dist.magnitude);
            if (inRange)
            {
                var dmg = crtSkill.damage;
                switch (crtSkill.id)
                {
                    case "sky fire":
                        player.health.TakeDamage(dmg);
                        //1.4f
                        break;

                    case "spike":
                        player.health.TakeDamage(dmg);
                        //1.35
                        break;

                    case "melee":
                        player.health.TakeDamage(dmg);
                        //
                        break;

                    case "melee minion":
                        player.health.TakeDamage(dmg);
                        //
                        break;
                }
            }
        }
    }

    void TickSkills()
    {
        if (crtSkill != null && _durationTimer > 0)
            _durationTimer -= Time.deltaTime;

        if (_durationTimer <= 0)
        {
            crtSkill = null;
            _durationTimer = 0;
        }

        foreach (var skl in skills)
        {
            if (skl.cdTimer > 0)
                skl.cdTimer -= Time.deltaTime;
        }
    }

    bool CanUseSkill(EnemySkill skl)
    {
        if (crtSkill != null && _durationTimer > 0)
            return false;

        if (skl.cdTimer > 0)
            return false;

        var playerPos = PlayerBehaviour.instance.transform.position;
        var dx = Mathf.Abs(playerPos.x - transform.position.x);
        if (dx < skl.minDist || dx > skl.maxDist)
            return false;

        return true;
    }
}