using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISoundTest : MonoBehaviour
{
    public void Click_Btn()
    {
        AkSoundEngine.PostEvent("test_UI", gameObject);
    }
}
