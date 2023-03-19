using System;
using AI.Manager;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace AI.Events
{
    public class AIHealtBarTarget : MonoBehaviour
    {
        public Transform targetObject;
        private AIController _targetObjectAIController;
        private GameObject _clonePool;
        public Image _ımage;
        private void Start()
        {
            _clonePool = GameObject.Find("<--ClonePool-->");
            gameObject.transform.SetParent(_clonePool.transform);
            _targetObjectAIController = targetObject.GetComponent<AIController>();
        }

        void FixedUpdate()
        {
            if (_targetObjectAIController.isDead)
            {
                //Server'dan da silmeli.
                PhotonNetwork.Destroy(gameObject);
            }
            else
            {
                
                TargetObjectFollow();
                TargetObjectHealthControl();
            }
            
        }

        private void TargetObjectFollow()
        {
            
            Vector3 takipEdilecekPozisyon = new Vector3(targetObject.localPosition.x, targetObject.localPosition.y +4, targetObject.position.z);
            transform.position = takipEdilecekPozisyon;
        }

        private void TargetObjectHealthControl()
        {
            if (_targetObjectAIController.enabled)
            {
                float healthUIValue = _targetObjectAIController.playerHealth / _targetObjectAIController.aiData.health;
                _ımage.fillAmount = healthUIValue;
                
            }
           

        }

        
    }
}
