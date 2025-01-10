using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour
{
    public float speed = 5.0f; // 控制移动速度

    void Update()
    {
        // 获取水平和垂直输入
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // 计算移动方向
        Vector3 movement = new Vector3(horizontal, vertical, 0);

        // 更新物体位置
        transform.position += movement * speed * Time.deltaTime;
    }
}
