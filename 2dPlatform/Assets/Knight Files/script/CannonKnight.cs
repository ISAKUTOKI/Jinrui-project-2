using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class CannonKnight : MonoBehaviour
{
    public FireBall prefab;
    public float interval;
    private Coroutine _currentCoroutine;

    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            if (_currentCoroutine != null)
                StopCoroutine(_currentCoroutine);
            _currentCoroutine = StartCoroutine(FireLoop(1, 0.3f));
        }
        if (Input.GetKeyDown("2"))
        {
            if (_currentCoroutine != null)
                StopCoroutine(_currentCoroutine);
            _currentCoroutine = StartCoroutine(FireLoop(2, 0.6f));
        }
        if (Input.GetKeyDown("3"))
        {
            if (_currentCoroutine != null)
                StopCoroutine(_currentCoroutine);
            _currentCoroutine = StartCoroutine(FireLoop(3, 1.2f));
        }
    }

    IEnumerator FireLoop(int a, float i)
    {
        yield return new WaitForSeconds(1);
        while (true)
        {
            Fire(a);
            yield return new WaitForSeconds(i);
        }
    }

    public float bulletDistance;
    void Fire(int count)
    {
        var upOffset = (count - 1f) / 2f * bulletDistance;
        for (int i = 0; i < count; i += 1)
        {
            var p = transform.position;
            p += (upOffset - i * bulletDistance) * Vector3.up;
            Instantiate(prefab, p, Quaternion.identity);
        }
    }


    //float modeOneInterval = 0.3f;
    //float modeTwoInterval = 0.6f;
    //float modeThreeInterval = 1.2f;
    ////float modeSwitchInterval = 1.0f;


    //IEnumerator FireLoop(float min, float max)
    //{
    //    while (true)
    //    {

    //        yield return new WaitForSeconds(modeSwitchInterval);
    //        for (int i = 0; i < 5; i += 1)
    //        {
    //            yield return new WaitForSeconds(modeOneInterval);
    //            Fire(1);
    //        }

    //        yield return new WaitForSeconds(modeSwitchInterval);
    //        for (int i = 0; i < 2; i += 1)
    //        {
    //            yield return new WaitForSeconds(modeTwoInterval);
    //            Fire(2);
    //        }

    //        yield return new WaitForSeconds(modeSwitchInterval);
    //        for (int i = 0; i < 1; i += 1)
    //        {
    //            yield return new WaitForSeconds(modeThreeInterval);
    //            Fire(3);
    //        }
    //    }
    //}





}
