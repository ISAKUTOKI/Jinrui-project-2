using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NekoGroundCheck : MonoBehaviour
{
    public bool isOnGround = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Time.time);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            isOnGround = false;
        }
    }

}
