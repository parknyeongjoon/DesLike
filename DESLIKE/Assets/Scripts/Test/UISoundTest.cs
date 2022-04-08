using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISoundTest : MonoBehaviour
{
    public void SoundTestBtn()
    {
        Debug.Log("UIBtn");
        AkSoundEngine.PostEvent("test_UI", gameObject);
    }
}