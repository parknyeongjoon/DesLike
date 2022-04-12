using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class c : b
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            StartCoroutine(GameObject.Find("B").GetComponent<b>().testAA());
        }
    }
}
