using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class c : b
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            GameObject.Find("B").SendMessage("testA");
        }
    }
}
