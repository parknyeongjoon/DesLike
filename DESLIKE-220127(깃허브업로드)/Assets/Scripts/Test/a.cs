using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class a : MonoBehaviour
{
    public delegate void TestHandler();
    public TestHandler testHandler;

    IEnumerator testCoroutineA;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            GameObject.Find("B").GetComponent<a>().plusTestA();
            GameObject.Find("B").GetComponent<a>().testHandler.Invoke();
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

    public void plusTestA()
    {
        testA();
        testHandler = testA;
    }
}
