using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSoundTest : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Idle");
        AkSoundEngine.PostEvent("test_Idle", gameObject);
    }
}
