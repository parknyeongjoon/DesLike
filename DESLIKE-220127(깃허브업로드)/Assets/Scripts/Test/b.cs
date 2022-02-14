using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class b : a
{
    void Start()
    {
        GameObject.Find("A").GetComponent<a>().testHandler += testBB;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            StartCoroutine(testBB());
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
