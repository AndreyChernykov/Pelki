using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class TreeGenerator : MonoBehaviour
{
    [SerializeField] SkeletonAnimation skeletonAnimation;
    [SerializeField] string[] skinsName;

    void Start()
    {
        int rnd = Random.Range(0, skinsName.Length);
        
        var skeleton = skeletonAnimation.Skeleton;
        
        var skeletonData = skeleton.Data;
        var mixAndMatchSkin = new Skin("");
        if(skinsName.Length > 0)mixAndMatchSkin.AddSkin(skeletonData.FindSkin(skinsName[rnd]));

        skeleton.SetSkin(mixAndMatchSkin);
        skeleton.SetSlotsToSetupPose();
    }

}
