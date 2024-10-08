using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraFollow : MonoBehaviour
{
    public Transform knight; // 目标物体（角色）
    public Vector3 offset = new Vector3(0, 0, -15); // 相对于目标物体的位置偏移
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        if (knight != null)
        {
            Vector3 desiredPosition = knight.position + offset;
            transform.position = desiredPosition;
        }
    }
}
