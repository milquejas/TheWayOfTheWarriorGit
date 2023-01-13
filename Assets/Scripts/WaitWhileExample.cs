using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitWhileExample : MonoBehaviour
{
    IEnumerator Start()
    {
        print("start called");
        yield return new WaitForSeconds(10);
        print("10 secs passed");

        //StartCoroutine(WaitWhile(0));
    }

    IEnumerator WaitWhile(int x)
    {
        yield return new WaitWhile(() => transform.position.x == 0);
        print("cube moved");
    }


}

