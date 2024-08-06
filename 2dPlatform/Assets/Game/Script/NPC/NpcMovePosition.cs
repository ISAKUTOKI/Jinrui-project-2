using UnityEngine;
using System.Collections;

public class NpcMovePosition : MonoBehaviour
{
    public Rigidbody2D rb { get; private set; }
    private Vector3 _movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
    }

    public void AddXMovement(Vector3 mm)
    {
        var v = rb.velocity;
        v.x = mm.x;
        rb.velocity = v;
    }

    public void StopXMovement()
    {
        var v = rb.velocity;
        v.x = 0;
        rb.velocity = v;
    }
}
