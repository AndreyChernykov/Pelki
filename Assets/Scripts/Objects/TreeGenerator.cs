using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class TreeGenerator : MonoBehaviour
{
    [SerializeField] SkeletonAnimation skeletonAnimation;
    [SerializeField] string[] skinsName;

    (float, float) speedAnimationRange = (0.8f, 1.2f);

    void Start()
    {

        int rnd = Random.Range(0, skinsName.Length);
        
        var skeleton = skeletonAnimation.Skeleton;
        
        var skeletonData = skeleton.Data;
        var mixAndMatchSkin = new Skin("");
        if(skinsName.Length > 0)mixAndMatchSkin.AddSkin(skeletonData.FindSkin(skinsName[rnd]));

        skeleton.SetSkin(mixAndMatchSkin);
        skeleton.SetSlotsToSetupPose();

        skeletonAnimation.timeScale = Random.Range(speedAnimationRange.Item1, speedAnimationRange.Item2);  
    }

}
