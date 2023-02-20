using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomMotion : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isAttacked;

    public void TakeDamage()
    {
        isAttacked = true;
    }
}
