using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class a : MonoBehaviour
{
    public delegate IEnumerator TestHandler();
    public TestHandler testHandler;

    IEnumerator testCoroutineA;

    void Start()
    {
        testCoroutineA = testAA();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(testCoroutineA);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StopCoroutine(testCoroutineA);
        }
    }
    protected virtual void testA()
    {
        Debug.Log("A");
    }

    public virtual IEnumerator testAA()
    {
        Debug.Log("AA코루틴 시작");
        for(int i = 0; i < 10; i++)
        {
            Debug.Log("AA");
            yield return new WaitForSeconds(1.0f);
        }
    }
}
