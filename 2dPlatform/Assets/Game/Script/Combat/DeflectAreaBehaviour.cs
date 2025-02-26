using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeflectAreaBehaviour : MonoBehaviour
{
    private List<Transform> hitSources;

    private void OnEnable()
    {
        hitSources = new List<Transform>();
    }

    private void OnDisable()
    {
        hitSources = new List<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.LogWarning("DeflectAreaBehaviour OnTriggerEnter2D " + collision.gameObject);
        var ene = collision.GetComponent<EnemyBehaviour>();
        if (ene != null)
        {
            if (!hitSources.Contains(ene.transform))
            {
                hitSources.Add(ene.transform);
            }
        }

        var MBA1 = collision.GetComponent<MeowbodyAttack1>();
        if (MBA1 != null)
        {
            if (!hitSources.Contains(MBA1.transform))
            {
                hitSources.Add(MBA1.transform);
            }
        }

        var MBA2 = collision.GetComponent<MeowbodyAttack2>();
        if (MBA2 != null)
        {
            if (!hitSources.Contains(MBA2.transform))
            {
                hitSources.Add(MBA2.transform);
            }
        }

        var MBFA = collision.GetComponent<MeowbodyFastAttack>();
        if (MBFA != null)
        {
            if (!hitSources.Contains(MBFA.transform))
            {
                hitSources.Add(MBFA.transform);
            }
        }

        var MBDA = collision.GetComponent<MeowbodyDiedAttack>();
        if (MBDA != null)
        {
            if (!hitSources.Contains(MBDA.transform))
            {
                hitSources.Add(MBDA.transform);
            }
        }

        var MBPFA = collision.GetComponent<MeowbodyPunishFastAttack>();
        if (MBPFA != null)
        {
            if (!hitSources.Contains(MBPFA.transform))
            {
                hitSources.Add(MBPFA.transform);
            }
        }

        var MLEA = collision.GetComponent<MusouloomExplosionDamageArea>();
        if (MLEA != null)
        {
            if (!hitSources.Contains(MLEA.transform))
            {
                hitSources.Add(MLEA.transform);
            }
        }
    }

    /// <summary>
    /// 最终的受到伤害的处理函数，接受的参数是伤害来源，可能的情况有
    /// 1 敌人身体，比如史莱姆的近战攻击，用的是整个敌人的transform判断弹反
    /// 2 飞射性物体projectile，也就是远程攻击，用的是敌人的子弹的transform判断弹反
    /// 3 敌人的身体部分，比如大型猫敌人的尾巴攻击，用的是这个攻击特定的攻击判定框的transform判断弹反
    /// </summary>
    /// <param name="hitSource"></param>
    public bool InAreaCheck(Transform hitSource)
    {
        return hitSources.Contains(hitSource);
    }
}
