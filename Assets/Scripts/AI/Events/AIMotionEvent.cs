using AI.Manager;
using Animation.AnimationManager;
using UnityEngine;
using UnityEngine.AI;

namespace AI.Events
{
    public class AIMotionEvent : MonoBehaviour
    {
        public void ControlAnimations(AIController currentObjectAI)
        {
            // currentObjectAI.navMeshAgent.velocity.sqrMagnitude > 0.1f
            if (!currentObjectAI.navMeshAgent.isStopped)
            {
                currentObjectAI.aiPlayerMotionType = AIType.AIPlayerMotionType.Run;
                AnimationController.Instance.ChangeAnimation(AnimationType.AnimationTypes.Run,currentObjectAI.animator);
            }
            else if(currentObjectAI.aiPlayerMotionType != AIType.AIPlayerMotionType.Attack)
            {
                currentObjectAI.aiPlayerMotionType = AIType.AIPlayerMotionType.Idle;
                AnimationController.Instance.ChangeAnimation(AnimationType.AnimationTypes.Idle,currentObjectAI.animator);
            }
            else if(currentObjectAI.aiPlayerMotionType == AIType.AIPlayerMotionType.Attack)
            {
                AnimationController.Instance.ChangeAnimation(AnimationType.AnimationTypes.Attack,currentObjectAI.animator);
            }
        }
    }
}
