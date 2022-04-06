using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPortBtn : MonoBehaviour
{
    public void SetPortBtn()
    {
        StartCoroutine(PortManager.Instance.SetSoldierCoroutine());
    }

    public void SetMutantBtn()
    {
        StartCoroutine(PortManager.Instance.SetMutantCoroutine());
    }
}
