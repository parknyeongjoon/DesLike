using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testRelicBtnScript : MonoBehaviour
{
    [SerializeField] RelicData testRelic;

    public void testGetRelic()
    {
        RelicManager.instance.GetRelic(testRelic.code);
    }
}
