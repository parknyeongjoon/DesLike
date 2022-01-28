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
            StartCoroutine(testAA());
            testA();
        }
    }
    protected virtual void testA()
    {
        Debug.Log("A");
    }

    IEnumerator testAA()
    {
        while (true)
        {
            Debug.Log("AA");
            yield return null;
        }
    }
}
