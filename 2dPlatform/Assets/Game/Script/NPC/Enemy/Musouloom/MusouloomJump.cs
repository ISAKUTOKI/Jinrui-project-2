using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusouloomJump : MonoBehaviour
{
    public void JumpOutFromEarth()
    {
        MusouloomBehaviour.instance.animator.SetTrigger("jumpOut");
        MusouloomBehaviour.instance.jumpSpot.canJumpOutFromEarth = false;
        StartCoroutine(JumpUpDownFromEarth(0.2f, 0.15f, -0.04f, 0.1f));
    }
    IEnumerator JumpUpDownFromEarth(float upAmount, float upTime, float downAmount, float downTime)
    {
        float upElapsedTime = 0;
        float downElapsedTime = 0;
        Vector3 startPosition = transform.position;
        Vector3 targetPositionUp = startPosition + new Vector3(0, upAmount, 0);
        Vector3 targetPositionDown = startPosition + new Vector3(0, -downAmount, 0);

        while (upElapsedTime < upTime)
        {
            transform.position = Vector3.Lerp(startPosition, targetPositionUp, (upElapsedTime / upTime));
            upElapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPositionUp;

        yield return new WaitForSeconds(0.05f);

        while (downElapsedTime < downTime)
        {
            transform.position = Vector3.Lerp(targetPositionUp, targetPositionDown, (downElapsedTime / downTime));
            downElapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPositionDown;
    }
    public IEnumerator ExplosionJump(float distance, float time)
    {
        float elapsedTime = 0;
        Vector3 startPosition = transform.position;
        while (elapsedTime < time)
        {
            transform.position = Vector3.Lerp(startPosition, startPosition + new Vector3(0, distance, 0), (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = startPosition + new Vector3(0, distance, 0); // 确保最终位置正确
    }
}
