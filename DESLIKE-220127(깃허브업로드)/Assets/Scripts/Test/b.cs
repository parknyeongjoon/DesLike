using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class b : a
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            testHandler?.Invoke();
        }
    }

    protected override void testA()
    {
        Debug.Log("B");
    }

    IEnumerator testBB()
    {
        Debug.Log("BB内风凭 矫累");
        yield return StartCoroutine(testAA());
        Debug.Log("BB内风凭 场");
    }

    
}
