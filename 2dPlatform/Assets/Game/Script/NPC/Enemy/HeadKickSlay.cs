using System.Collections;
using UnityEngine;

public class HeadKickSlay : MonoBehaviour
{
    public Transform headTrans;
    public float headCircleSize;
    public int damage = 10;

    private void Start()
    {
        if (headTrans == null)
            headTrans = transform;
    }
    public bool CheckHit(Vector2 point)
    {
        //var point = collision.contacts[0].point;
        Vector2 headPosVec2 = headTrans.position;
        var dist = Vector2.Distance(point, headPosVec2);
        //Debug.Log("dist " + dist);
        if (dist < headCircleSize)
            return true;

        return false;
    }
}