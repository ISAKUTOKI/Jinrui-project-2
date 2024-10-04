using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAnimationEvent : MonoBehaviour
{
    public KnightAttack ka;
    public KnightJumpAttack kja;
    public KnightClothesColor kcc;
    public void OnAttack()
    {
        ka.AttackHit();
    }
    public void OnHurt()
    {
        kcc.GetClothesColorOn();
    }
}
