using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Shaman : MonoBehaviour
{
    
    [SerializeField] SkeletonAnimation skeletonAnimation;
    [SerializeField] BoxCollider2D boxCollider;
    [SerializeField] float delayPlayAnimation;

    Spine.AnimationState spineAnimationState;

    enum State
    {
        idle,
        idle_atack,
    }

    void Start()
    {
        spineAnimationState = skeletonAnimation.AnimationState;

        
    }

    IEnumerator ActivateShaman()
    {
        boxCollider.enabled = false;
        spineAnimationState.SetAnimation(0, State.idle_atack.ToString(), false);
        yield return new WaitForSeconds(delayPlayAnimation);
        spineAnimationState.SetAnimation(0, State.idle.ToString(), true);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            StartCoroutine(ActivateShaman());
        }
    }
}
