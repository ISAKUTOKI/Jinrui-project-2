using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightDefense : MonoBehaviour
{
    private Rigidbody2D rb;
    public Animator animator;
    public float defenseAngle = 180f;
    public float defenseCD = 0.8f;
    public float defenseDuration = 3.0f;
    float lastDefenseTime;

    public KnightGroundCheck kgc;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    bool canDefense()
    {
        //不在空中
        //不在受伤
        //不在防御CD
        if (!kgc.isOnGround)
            return false;
        if (IsHurting())
            return false;
        if (lastDefenseTime < defenseCD)
            return false;


        return true;

    }

    bool IsHurting()
    {
        if (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Hurt Animation")
        {
            return true;
        }
        return false;
    }

    void DefenseStart()
    {
        lastDefenseTime = Time.time;
    }


}
