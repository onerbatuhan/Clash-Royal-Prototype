using System;
using System.Collections;
using AI.Manager;
using Animation.AnimationManager;
using Object;
using Teams.Manager;
using UnityEngine;

namespace AI.Events
{
    public class AIDeadEvent : MonoBehaviour
    {
        
        private bool _isDead;
        public ObjectPool objectPool;

        private void Start()
        {
            objectPool = GameObject.Find("DeadEffectPooler").GetComponent<ObjectPool>();
        }

        public void DeadControl(AIController currentObject)
        {
            if (!(currentObject.playerHealth <= 0) || _isDead) return;
            _isDead = true;
            currentObject.isDead = _isDead;
            Dead(currentObject);
           

        }

        private void Dead(AIController currentObject)
        {
            TeamController.Instance.allPlayer.Remove(currentObject.gameObject);
            currentObject.aiPlayerMotionType = AIType.AIPlayerMotionType.Dead;
            AnimationController.Instance.ChangeAnimation(AnimationType.AnimationTypes.Dead,currentObject.animator);
           
            StartCoroutine("start",currentObject);
        }

        private IEnumerator start(AIController currentObject)
        {
               GameObject vfx =  objectPool.GetPooledEffectObject();
               vfx.SetActive(true);
               vfx.transform.position = transform.position;
            yield return new WaitForSeconds(.7f);
            currentObject.gameObject.SetActive(false);
            currentObject.enabled = false;
            currentObject.navMeshAgent.enabled = false;
            
        }
    }
}
