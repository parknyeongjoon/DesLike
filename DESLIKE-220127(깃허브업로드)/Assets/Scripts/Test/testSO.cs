using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testSO : ScriptableObject
{
    public void TestSO(int testInt)
    {
        testInt++;
        Debug.Log(testInt);
    }
}
