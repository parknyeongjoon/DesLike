using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class a : MonoBehaviour
{
    public delegate void TestHandler();
    public TestHandler testHandler;

    IEnumerator testCoroutineA;
    Coroutine testCoroutine;

    [SerializeField]
    List<Coroutine> coroutines = new List<Coroutine>();

    [SerializeField]
    testSO testSO;

    void Start()
    {
        Debug.Log(testSO.testString);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(testAA());
        }
    }
    protected virtual void testA()
    {
        Debug.Log("A");
    }

    public virtual IEnumerator testAA()
    {
        float testTime = 0.1f;
        while (testTime > 0)
        {
            yield return yieldTest();
            Debug.Log("AA");
            testTime -= Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator yieldTest()
    {
        yield return new WaitForSeconds(1.0f);
    }
}
