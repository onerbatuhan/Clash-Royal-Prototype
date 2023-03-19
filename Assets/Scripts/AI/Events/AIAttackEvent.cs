using System;
using AI.Manager;
using Animation.AnimationManager;
using Object;
using Photon.Pun;
using Tower.Manager;
using UnityEngine;

namespace AI.Events
{
    public class AIAttackEvent : MonoBehaviour
    {
        public AIController currentEnemyObject;
        public AIController currentObjectAI;
        private GameObject _referenceObject;
        public GameObject currentTowerObject;
        public ObjectPool vfxPool;
        private void Start()
        {
            ObjectPoolReferenceAdd();
            _referenceObject = gameObject;
        }

        public void LaunchTheAttackEnemy()
        {
            if (_referenceObject != currentEnemyObject.gameObject)
            {
                _referenceObject = currentEnemyObject.gameObject;
                currentObjectAI.aiPlayerMotionType = AIType.AIPlayerMotionType.Attack;
                AnimationController.Instance.ChangeAnimation(AnimationType.AnimationTypes.Attack,currentObjectAI.animator);
            }
        }

        public void LaunchTheAttackTower()
        {
            if (_referenceObject != currentTowerObject.gameObject)
            {
                _referenceObject = currentTowerObject.gameObject;
                currentObjectAI.aiPlayerMotionType = AIType.AIPlayerMotionType.Attack;
                AnimationController.Instance.ChangeAnimation(AnimationType.AnimationTypes.Attack,currentObjectAI.animator);
            }
        }
        
       
        public void Attack() //Attack AnimationClip Event
        {
            if (currentTowerObject != null)
            {
                VfxShow();
                currentTowerObject.GetComponent<TowerController>().health -= currentObjectAI.aiData.damage;
            }
            else if (currentEnemyObject != null)
            {
                VfxShow();
                currentEnemyObject.playerHealth -= currentObjectAI.aiData.damage;
            }
           
            
        }

        private void ObjectPoolReferenceAdd()
        {
            
            switch (transform.GetComponent<AIController>().aiPlayerType)
            {
                case AIType.AIPlayerType.Melee:
                    vfxPool = GameObject.Find("MeleeAttackEffectPooler").GetComponent<ObjectPool>();
                    break;
                case AIType.AIPlayerType.Aerial:
                    vfxPool = GameObject.Find("AerialAttackEffectPooler").GetComponent<ObjectPool>();
                    break;
                case AIType.AIPlayerType.Range:
                    vfxPool = GameObject.Find("RangeAttackEffectPooler").GetComponent<ObjectPool>();
                    break;
                case AIType.AIPlayerType.Support:
                    break;
                case AIType.AIPlayerType.Tower:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        private void VfxShow()
        {
            //PhotonPun'da objectPool konusuna gelince açarız. Şimdilik kapalı dursun. Ama masterClient'ın objectPool objelerini klonlayacağını unutma. Çünkü, tüm client'lar objectPool oluşturursa server kapasitesi şişer.
            //Sadece odayı kuran client(masterClient) objectPool yapacak, diğer clientlar da o objectPool'a erişecek.
            
            // GameObject vfx = vfxPool.GetPooledEffectObject();
            // vfx.SetActive(true);
            // vfx.transform.position = transform.position;
            // vfx.transform.rotation = transform.rotation;
        }
        
    }
}
