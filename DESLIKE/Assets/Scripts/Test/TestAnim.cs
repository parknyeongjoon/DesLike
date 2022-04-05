using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class TestAnim : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset[] animClips;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            skeletonAnimation.state.SetAnimation(0, animClips[0], true);//idle
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            skeletonAnimation.state.SetAnimation(0, animClips[1], true);//idle
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            skeletonAnimation.state.SetAnimation(0, animClips[2], true);//idle
        }
    }
}
