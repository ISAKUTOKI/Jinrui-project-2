using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightClothesColor : MonoBehaviour
{
    Animator animator;
    public GameObject[] clothesColorAnims; // 不同版本的ClothesColor动画
    public float colorChangeIntensity = 0.2f; // 颜色变化强度
    public float positionChangeIntensity = 0.1f; // 位置变化强度
    public float rotationChangeIntensity = 10f; // 旋转变化强度

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetClothesColorOn()
    {


        // 随机选择一个ClothesColor动画
        int randomIndex = Random.Range(0, clothesColorAnims.Length);
        GameObject selectedAnim = clothesColorAnims[randomIndex];
        // 创建动画实例
        GameObject effectInstance = Instantiate(selectedAnim, transform.position, Quaternion.identity);

        // 获取动画实例的Animator组件
        Animator clothesColorAnimator = effectInstance.GetComponent<Animator>();

        // 应用随机的颜色变化
        Color effectColor = new Color(
            Random.Range(1 - colorChangeIntensity, 1 + colorChangeIntensity),
            Random.Range(1 - colorChangeIntensity, 1 + colorChangeIntensity),
            Random.Range(1 - colorChangeIntensity, 1 + colorChangeIntensity)
        );
        effectInstance.GetComponent<SpriteRenderer>().color = effectColor;

        // 应用随机的位置变化
        Vector3 effectPosition = transform.position + new Vector3(
            Random.Range(-positionChangeIntensity, positionChangeIntensity),
            Random.Range(-positionChangeIntensity, positionChangeIntensity),
            0
        );
        effectInstance.transform.position = effectPosition;

        // 应用随机的旋转变化
        Quaternion effectRotation = Quaternion.Euler(0, 0, Random.Range(-rotationChangeIntensity, rotationChangeIntensity));
        effectInstance.transform.rotation = effectRotation;
    }
}
