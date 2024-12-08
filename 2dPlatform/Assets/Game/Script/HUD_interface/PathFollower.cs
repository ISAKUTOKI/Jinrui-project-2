using UnityEngine;

public class PathFollower : MonoBehaviour
{
    public Transform[] pathPoints; // 路径点数组
    public Transform target;

    void Update()
    {
        // 找到x轴上最接近的两个路径点
        int closestPoint1Index = 0;
        int closestPoint2Index = 0;
        var x = target.position.x;

        for (int i = 0; i < pathPoints.Length - 1; i++)
        {
            var _p1 = pathPoints[i].position;
            var _p2 = pathPoints[i + 1].position;
            var _x1 = _p1.x;
            var _x2 = _p2.x;
            if (x < _x1 && i == 0)
            {

                transform.position = new Vector3(x, _p1.y, _p1.z);
                transform.LookAt(target);
                return;
            }
            if (x > _x2 & i == pathPoints.Length - 2)
            {
                transform.position = new Vector3(x, _p2.y, _p2.z);
                transform.LookAt(target);
                return;
            }
            if (x >= _x1 && x <= _x2)
            {
                closestPoint1Index = i;
                closestPoint2Index = i + 1;
                break;
            }
        }

        var p1 = pathPoints[closestPoint1Index].position;
        var p2 = pathPoints[closestPoint2Index].position;
        var x1 = p1.x;
        var x2 = p2.x;

        float lerpFactor = (x - x1) / (x2 - x1);
        var p = Vector3.Lerp(p1, p2, lerpFactor);
        p.x = x;

        transform.position = p;
        transform.LookAt(target);
    }
}