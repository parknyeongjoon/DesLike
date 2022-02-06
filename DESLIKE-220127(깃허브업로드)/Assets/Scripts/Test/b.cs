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
            testA();
            StartCoroutine(testAA());
        }
    }

    protected override void testA()
    {
        Debug.Log("B");
    }

    IEnumerator testBB()
    {
        yield return new WaitForSeconds(1.0f);
        Debug.Log("BB");
        yield return new WaitForSeconds(2.0f);
    }

    public override IEnumerator testAA()
    {
        Debug.Log("B에서 AA");
        yield return StartCoroutine(base.testAA());
        Debug.Log("B에서 AA끝남");
    }
}
