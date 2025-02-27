using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeflectAreaBehaviour : MonoBehaviour
{
    private List<Collider2D> hitSources; // 改为记录 Collider2D
    private Collider2D _deflectCollider; // 弹反区域的 Collider2D

    private void Awake()
    {
        _deflectCollider = GetComponent<Collider2D>(); // 获取弹反区域的 Collider2D
        if (_deflectCollider == null)
        {
            Debug.LogError("DeflectAreaBehaviour: No Collider2D found on this GameObject!");
        }
    }

    private void OnEnable()
    {
        hitSources = new List<Collider2D>();
    }

    private void OnDisable()
    {
        hitSources = new List<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var ENE = collision.GetComponent<EnemyBehaviour>();
        if (ENE != null)
        {
            if (!hitSources.Contains(collision))
            {
                hitSources.Add(collision);
            }
        }
        var MSEA = collision.GetComponent<MusouloomExplosionDamageArea>();
        if (MSEA != null)
        {
            if (!hitSources.Contains(collision))
            {
                hitSources.Add(collision);
            }
        }
        var MBA1 = collision.GetComponent<MeowbodyAttack1>();
        if (MBA1 != null)
        {
            if (!hitSources.Contains(collision))
            {
                hitSources.Add(collision);
            }
        }
        var MBA2 = collision.GetComponent<MeowbodyAttack2>();
        if (MBA2 != null)
        {
            if (!hitSources.Contains(collision))
            {
                hitSources.Add(collision);
            }
        }
        var MBFA = collision.GetComponent<MeowbodyFastAttack>();
        if (MBFA != null)
        {
            if (!hitSources.Contains(collision))
            {
                hitSources.Add(collision);
            }
        }
        var MBDA = collision.GetComponent<MeowbodyDiedAttack>();
        if (MBDA != null)
        {
            if (!hitSources.Contains(collision))
            {
                hitSources.Add(collision);
            }
        }
        var MBPFA = collision.GetComponent<MeowbodyPunishFastAttack>();
        if (MBPFA != null)
        {
            if (!hitSources.Contains(collision))
            {
                hitSources.Add(collision);
            }
        }
        var MLEA = collision.GetComponent<MusouloomExplosionDamageArea>();
        if (MLEA != null)
        {
            if (!hitSources.Contains(collision))
            {
                hitSources.Add(collision);
            }
        }
    }

    /// <summary>
    /// 判断伤害来源是否在弹反区域内
    /// </summary>
    /// <param name="hitSourceCollider">伤害来源的 Collider2D</param>
    public bool InAreaCheck(Collider2D hitSourceCollider)
    {
        if (hitSourceCollider == null || _deflectCollider == null)
            return false;

        // 检测两个 Collider 是否重叠
        return _deflectCollider.IsTouching(hitSourceCollider);
    }
}