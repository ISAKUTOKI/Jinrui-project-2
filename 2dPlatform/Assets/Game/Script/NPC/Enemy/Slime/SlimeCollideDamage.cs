using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeCollideDamage : MonoBehaviour
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
        //造成伤害
        PlayerBehaviour.instance.health.TakeDamage(1);
        _lastDealDamageTime = Time.time;
    }

    void PlayerIsInCheckArea(float radius)
    {
        var a = Physics2D.OverlapCircleAll(transform.position, radius);//以坐标为中心，半径
        for (int i = a.Length - 1; i >= 0; i--)
        {
            if (a[i].gameObject.CompareTag("Player"))
            {
                Debug.Log("玩家在史莱姆伤害范围内");
                DealDamage();
                break;
            }
        }
    }
}
