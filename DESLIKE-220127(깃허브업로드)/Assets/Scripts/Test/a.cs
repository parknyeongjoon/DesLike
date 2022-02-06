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

        }
    }
    protected virtual void testA()
    {
        Debug.Log("A");
    }

    public virtual IEnumerator testAA()
    {
        while (!Input.GetKey(KeyCode.A))
        {
            Debug.Log("AA");
            yield return null;
        }
    }
}
