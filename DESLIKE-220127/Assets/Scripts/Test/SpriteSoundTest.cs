using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSoundTest : MonoBehaviour
{
    void Start()
    {
        AkSoundEngine.PostEvent("test_Sound", gameObject);
    }
}
