using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitUntilPosition : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(WaitUntilXisFive());
    }

    private void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime);
    }

    IEnumerator WaitUntilXisFive()
    {
        yield return new WaitUntil(() => transform.position.x >= 5);
        print("Cuben x >=5");
    }


}
