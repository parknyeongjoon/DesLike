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

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            StopCoroutine(testCoroutine);
        }
    }
    protected virtual void testA()
    {
        Debug.Log("A");
    }

    public virtual IEnumerator testAA()
    {
        while (true)
        {
            Debug.Log("AA");
            yield return null;
        }
    }
}
