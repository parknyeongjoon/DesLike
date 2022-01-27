using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class a : MonoBehaviour
{
    public delegate IEnumerator TestHandler();
    public TestHandler testHandler;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(testHandler());
            testA();
        }
    }
    protected virtual void testA()
    {
        Debug.Log("A");
    }

    IEnumerator testAA()
    {
        yield return new WaitForSeconds(1.0f);
        Debug.Log("AA");
        yield return new WaitForSeconds(2.0f);
    }
}
