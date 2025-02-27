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
        // 获取史莱姆的 Collider2D
        var slimeCollider = GetComponent<Collider2D>();
        if (slimeCollider != null)
        {
            // 造成伤害，并传递史莱姆的 Collider2D 作为伤害来源
            PlayerBehaviour.instance.OnHit(slimeCollider);
        }
        else
        {
            Debug.LogError("SlimeColliderDamage: No Collider2D found on this GameObject!");
        }

        _lastDealDamageTime = Time.time; // 记录本次伤害的时间
    }

    void PlayerIsInCheckArea(float radius)
    {
        var a = Physics2D.OverlapCircleAll(transform.position, radius);//以坐标为中心，半径
        for (int i = a.Length - 1; i >= 0; i--)
        {
            if (a[i].gameObject.CompareTag("Player"))
            {
                //Debug.Log("玩家在史莱姆伤害范围内");
                DealDamage();
                break;
            }
        }
    }
}
