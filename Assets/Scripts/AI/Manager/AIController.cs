using System;
using AI.Events;
using AI.ScriptTable;
using Photon.Pun;
using Teams.Manager;
using UnityEngine;
using UnityEngine.AI;

namespace AI.Manager
{
    public class AIController : MonoBehaviour
    {
        [Header("[Events]")]
        public AITargetEvent targetEvent;
        public AIMotionEvent motionEvent;
        private AIAttackEvent _attackEvent;
        private AIDeadEvent _deadEvent;
        [Header("[Player Components]")]
        public NavMeshAgent navMeshAgent;
        public Animator animator;
        [Header("[Player About]")] 
        public AIType.AIPlayerType aiPlayerType;
        public AIType.AIPlayerMotionType aiPlayerMotionType;
        [Header("[Player Attributes]")] 
        private Teams.Manager.Team _team;
        public AIData aiData;
        public float playerHealth;
        public bool isDead;
        


        private void Awake()
        {
            
            HealthBarAddSpriteObject();
        }

        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            _team = GetComponent<Teams.Manager.Team>();
            _attackEvent = GetComponent<AIAttackEvent>();
            _deadEvent = GetComponent<AIDeadEvent>();
            playerHealth = aiData.health;
            animator.speed = aiData.attackSpeed;
            


        }
        

        private void Update()
        {
            NavmeshFollowTarget();
            motionEvent.ControlAnimations(this.GetComponent<AIController>());
            _deadEvent.DeadControl(this.GetComponent<AIController>());
        }

        private void NavmeshFollowTarget()
        {
            GameObject targetObject = targetEvent.FindClosestTargetObject(navMeshAgent);
            if (targetObject == null) return;
            float distanceToTarget = Vector3.Distance(transform.position, targetObject.transform.position);
            if (distanceToTarget <= aiData.attackRange)
            {
                
                AttackTarget(targetObject);
            }
            else
            {
                StopAttacking(targetObject.transform.position);
            }
           
        }
        
        
        private void AttackTarget(GameObject enemyObject)
        {
            
            AIController currentEnemyAIController = enemyObject.GetComponent<AIController>();
            if (currentEnemyAIController!=null)
            {
                navMeshAgent.isStopped = true;
                _attackEvent.currentEnemyObject = currentEnemyAIController;
                _attackEvent.currentObjectAI = this;
                _attackEvent.LaunchTheAttackEnemy();
                transform.LookAt(new Vector3(enemyObject.transform.position.x,transform.position.y,enemyObject.transform.position.z));
            }
            else
            {
                navMeshAgent.isStopped = true;
                _attackEvent.currentObjectAI = this;
                _attackEvent.currentTowerObject = enemyObject;
                _attackEvent.LaunchTheAttackTower();
            }
           
        }
        private void StopAttacking(Vector3 targetPosition)
        {
            navMeshAgent.isStopped = false;
           
            
            navMeshAgent.SetDestination(targetPosition);
            
        }

        private void HealthBarAddSpriteObject()
        {
            GameObject healthBarObjectSprite = PhotonNetwork.Instantiate(aiData.healthBarObjectSprite.name,transform.position , aiData.healthBarObjectSprite.transform.rotation);
            healthBarObjectSprite.GetComponent<AIHealtBarTarget>().targetObject = gameObject.transform;

        }
        
        
    }
}

