using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCamp : MonoBehaviour
{
    void Awake()
    {
        AkSoundEngine.PostEvent("Music_S1_Base_Camp", gameObject);
    }
}
