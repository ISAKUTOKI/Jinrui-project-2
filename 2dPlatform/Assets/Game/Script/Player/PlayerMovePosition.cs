using UnityEngine;
using System.Collections;

public class PlayerMovePosition : MonoBehaviour
{
    public Rigidbody2D rb { get; private set; }
    private Vector3 _movement;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
    }

    public void AddXMovement(Vector3 mm)
    {
        //Debug.Log("AddXMovement");
        var v = rb.velocity;
        v.x = mm.x;
        rb.velocity = v;
    }

    public void StopXMovement()
    {
        if (PlayerBehaviour.instance.health.isDead)
            return;
        //Debug.Log("StopXMovement");
        var v = rb.velocity;
        v.x = 0;
        rb.velocity = v;
    }
}