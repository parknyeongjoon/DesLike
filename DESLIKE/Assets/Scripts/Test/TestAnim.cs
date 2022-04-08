using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class TestAnim : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            skeletonAnimation.state.AddAnimation(0, "idle", true, 0);//idle
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            skeletonAnimation.state.AddAnimation(0, "move", true, 0);//idle
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            skeletonAnimation.state.SetAnimation(0, "skill_1", false);//idle
        }
    }
}
