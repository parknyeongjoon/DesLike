using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    public UnityEvent<float> testUnityEvent;

    void Start()
    {
        testUnityEvent.AddListener(PrintA);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            testUnityEvent?.Invoke(5.0f);
        }
    }

    public void PrintA(float a)
    {
        Debug.Log(a);
    }

    public void PrintB()
    {
        Debug.Log("B");
    }

    public void PrintC()
    {
        Debug.Log("C");
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
