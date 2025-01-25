using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSpotImageBehaviour : MonoBehaviour
{
    EnemyBehaviour enemyBehaviour;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        enemyBehaviour = GetComponentInParent<EnemyBehaviour>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyBehaviour.isInTest == true)
        {
            spriteRenderer.enabled = false;
        }
    }
}
