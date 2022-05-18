using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class a : MonoBehaviour
{
    Action<float> testAction;

    void Start()
    {
        testAction += floatTest;
        PrintFloat(2.0f);
    }

    void PrintFloat(float printFloat)
    {
        Debug.Log(printFloat);
        testAction?.Invoke(printFloat);
        Debug.Log(printFloat);
    }

    void floatTest(float testFloat)
    {
        Debug.Log("µ¡¼À");
        testFloat++;
    }
}
