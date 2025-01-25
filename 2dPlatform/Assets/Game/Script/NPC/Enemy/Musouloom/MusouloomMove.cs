using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusouloomMove : MonoBehaviour
{
    [HideInInspector] public bool canSearch;
    float moveSpeed = 2.5f;
    // Start is called before the first frame update
    void Start()
    {
        canSearch=false;
    }
    // Update is called once per frame
    void Update()
    {
        TryToSearch();
    }
    public void TryToSearch()
    {
        if (canSearch)
        {
            MusouloomBehaviour.instance.health.canDie = true;
            Vector3 playerPosition = MusouloomBehaviour.instance.Neko.transform.position;
            float directionToPlayerX = playerPosition.x - transform.position.x;
            Vector3 moveDirection = new Vector3(moveSpeed * Time.deltaTime, 0, 0);
            if (directionToPlayerX != 0)
            {
                moveDirection.x *= directionToPlayerX / Mathf.Abs(directionToPlayerX);
            }
            transform.position = transform.position + moveDirection;

            if (directionToPlayerX > 0)
                FlipRight();
            if (directionToPlayerX < 0)
                FlipLeft();
        }
    }
    void FlipLeft()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }
    void FlipRight()
    {
        transform.localScale = new Vector3(-1, 1, 1);
    }
}
