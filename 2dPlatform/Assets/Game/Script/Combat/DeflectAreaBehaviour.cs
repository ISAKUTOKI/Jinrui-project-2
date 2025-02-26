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
    /// ���յ��ܵ��˺��Ĵ����������ܵĲ������˺���Դ�����ܵ������
    /// 1 �������壬����ʷ��ķ�Ľ�ս�������õ����������˵�transform�жϵ���
    /// 2 ����������projectile��Ҳ����Զ�̹������õ��ǵ��˵��ӵ���transform�жϵ���
    /// 3 ���˵����岿�֣��������è���˵�β�͹������õ�����������ض��Ĺ����ж����transform�жϵ���
    /// </summary>
    /// <param name="hitSource"></param>
    public bool InAreaCheck(Transform hitSource)
    {
        return hitSources.Contains(hitSource);
    }
}
