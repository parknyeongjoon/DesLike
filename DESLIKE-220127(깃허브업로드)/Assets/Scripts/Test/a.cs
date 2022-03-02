using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class a : MonoBehaviour
{
    public delegate void TestHandler();
    public TestHandler testHandler;

    IEnumerator testCoroutineA;

    [SerializeField]
    List<Coroutine> coroutines = new List<Coroutine>();

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            coroutines.Add(StartCoroutine(testAA()));
        }
        Debug.Log(coroutines.Count);
        if(coroutines[0] != null)
        {
            Debug.Log("³Î ¾Æ´Ô");
        }
    }
    protected virtual void testA()
    {
        Debug.Log("A");
    }

    public virtual IEnumerator testAA()
    {
        yield return new WaitForSeconds(2.0f);
    }
}
