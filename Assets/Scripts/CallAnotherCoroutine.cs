using System.Collections;
using UnityEngine;

public class CallAnotherCoroutine : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(First());
    }

    IEnumerator First()
    {
        print("starting First");
        yield return new WaitForSeconds(3);
        print("calling Second..");
        StartCoroutine(Second());
    }
    IEnumerator Second()
    {
        print("starting Second");
        yield return null;
    }




}
