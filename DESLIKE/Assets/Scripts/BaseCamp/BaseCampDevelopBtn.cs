using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCampDevelopBtn : MonoBehaviour
{
    public void SoldierBtn()
    {
        StartCoroutine(PortManager.Instance.SetSoldierCoroutine());
    }

    public void MutantBtn()
    {
        StartCoroutine(PortManager.Instance.SetMutantCoroutine());
    }
}
