using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallEveryFiveSeconds : MonoBehaviour
{
    int count; //how many times routine has been called
    IEnumerator coroutine;

    void Start()
    {
        count = 0;
        coroutine = LogEveryFiveSeconds();
        StartCoroutine(coroutine);
    }

    IEnumerator LogEveryFiveSeconds()
    {
        while (true) //infinite loop
        {
            yield return new WaitForSeconds(5);
            count++;
            print("5 seconds has passsed " + count + " times");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            StopCoroutine(coroutine);
            print("coroutine stopped");
        }
    }


}