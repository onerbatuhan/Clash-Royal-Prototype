using System;
using DesignPattern;
using UnityEngine;

namespace Animation.AnimationManager
{
    public class AnimationController : Singleton<AnimationController>
    {
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private static readonly int IsAttack = Animator.StringToHash("IsAttack");
        private static readonly int IsDead = Animator.StringToHash("IsDead");

        public void ChangeAnimation(AnimationType.AnimationTypes animationTypes,Animator currentAnimator)
        {
            if(animationTypes == AnimationType.AnimationTypes.Dead) return;
            switch (animationTypes)
            {
                case AnimationType.AnimationTypes.Idle:
                    currentAnimator.SetBool(IsAttack,false);
                    currentAnimator.SetBool(IsMoving,false);
                    break;
                case AnimationType.AnimationTypes.Run:
                    currentAnimator.SetBool(IsAttack,false);
                    currentAnimator.SetBool(IsMoving,true);
                    break;
                case AnimationType.AnimationTypes.Attack:
                    currentAnimator.SetBool(IsMoving,false);
                    currentAnimator.SetBool(IsAttack,true);
                    break;
                case AnimationType.AnimationTypes.Dead:
                    currentAnimator.SetBool(IsDead,true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(animationTypes), animationTypes, null);
            }
        }
    }
}
