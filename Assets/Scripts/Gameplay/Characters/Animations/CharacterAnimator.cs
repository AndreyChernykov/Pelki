using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Pelki.Gameplay.Characters.Movements;

namespace Pelki
{
    public class CharacterAnimator : MonoBehaviour
    {

        [SerializeField] SkeletonAnimation skeletonAnimation;
        [SerializeField] GroundMover groundMover;

        AnimationPlayer animationPlayer;

        Spine.AnimationState spineAnimationState;


        public enum AnimationPlayer
        {
            idle,
            run,
            run_jump,
            jump_down,
            jump_up_state,
            jump_up_to_down,
            cast_fireball,
            kick,
        }

        private void Start()
        {
            spineAnimationState = skeletonAnimation.AnimationState;

            
        }

        private void SetState()
        {
            spineAnimationState.SetAnimation(0, animationPlayer.ToString(), true);
        }

        private void Update()
        {
            if (!groundMover.IsGrounded) 
            {
                animationPlayer = AnimationPlayer.run;
                SetState();
            } 
            
        }

    }
}
