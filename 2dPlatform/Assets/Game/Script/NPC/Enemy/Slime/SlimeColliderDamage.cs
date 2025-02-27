using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeColliderDamage : MonoBehaviour
{
    [SerializeField] float DamageCD = 0.5f;
    private float _lastDealDamageTime;
    [SerializeField] float checkAreaRadius = 0.3f;

    void Update()
    {
        if (Time.time - _lastDealDamageTime > DamageCD)
        {
            PlayerIsInCheckArea(checkAreaRadius);
        }
    }

    void DealDamage()
    {
        // ��ȡʷ��ķ�� Collider2D
        var slimeCollider = GetComponent<Collider2D>();
        if (slimeCollider != null)
        {
            // ����˺���������ʷ��ķ�� Collider2D ��Ϊ�˺���Դ
            PlayerBehaviour.instance.OnHit(slimeCollider);
        }
        else
        {
            Debug.LogError("SlimeColliderDamage: No Collider2D found on this GameObject!");
        }

        _lastDealDamageTime = Time.time; // ��¼�����˺���ʱ��
    }

    void PlayerIsInCheckArea(float radius)
    {
        var a = Physics2D.OverlapCircleAll(transform.position, radius);//������Ϊ���ģ��뾶
        for (int i = a.Length - 1; i >= 0; i--)
        {
            if (a[i].gameObject.CompareTag("Player"))
            {
                //Debug.Log("�����ʷ��ķ�˺���Χ��");
                DealDamage();
                break;
            }
        }
    }
}
