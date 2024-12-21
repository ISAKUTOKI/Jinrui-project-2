using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDie : MonoBehaviour
{
    // Start is called before the first frame update

    public float slowdownSpeed = 0.01f;

    // 当前时间缩放的值
    private float currentScale = 1.0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F12))
        {
            EngineTimeScale();
            Debug.Log("slowing");
        }
    }

    void Die()
    {

        PlayerBehaviour.instance.animator.SetTrigger("die");
    }

    void EngineTimeScale()
    {
        if (currentScale > 0)
        {
            // 逐渐减少时间缩放值
            currentScale = Mathf.Max(0, currentScale - slowdownSpeed * Time.deltaTime);
            // 更新Time.timeScale
            Time.timeScale = currentScale;
        }
    }
}
