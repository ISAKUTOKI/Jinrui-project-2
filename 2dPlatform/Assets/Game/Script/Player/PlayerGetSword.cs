using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGetSword : MonoBehaviour
{
    Animator animator;

    float tapTimer = 0.0f;
    float tapDuration = 0.3f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TryToGetSword()
    {
        animator.SetBool("tryToGetSword", true);
    }
    public void TappingJKey()
    {
        animator.SetBool("isGettingSword", true);
        tapTimer = Time.time;
        if (tapTimer - Time.time > tapDuration)
            animator.SetBool("isGettingSword", false);

    }
    public void FailToGetSword()
    {
        animator.SetBool("tryToGetSword", false);
    }
}