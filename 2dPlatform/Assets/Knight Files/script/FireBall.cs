using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float speed;
    public Vector3 direction = Vector3.right;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)//外置 满足条件直接触发
                                                       //此处条件为 此碰撞箱进入了另一个碰撞箱
    {
        var da = collision.GetComponent<DeflectArea>();

        if (da != null)
        {
            da.targets.Add(this);
        }


        var km = collision.GetComponent<KnightMove>();

        if (km != null)
        {
            km.Hurt();
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)//外置 满足条件直接触发
                                                      //此处条件为 此碰撞箱离开了另一个碰撞箱
    {
        var da = collision.GetComponent<DeflectArea>();

        if (da != null)
        {
            da.targets.Remove(this);
        }
    }

    public void Reverse()
    {
        direction = -direction;
        var s = transform.localScale;
        s.x = -s.x;
        transform.localScale = s;
    }

}
