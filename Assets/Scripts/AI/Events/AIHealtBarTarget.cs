using System;
using AI.Manager;
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

        void LateUpdate()
        {
            if (_targetObjectAIController.isDead)
            {
                Destroy(gameObject);
            }
            else
            {
                
                TargetObjectFollow();
                TargetObjectHealthControl();
            }
            
        }

        private void TargetObjectFollow()
        {
            Vector3 position = transform.position;
            Vector3 takipEdilecekPozisyon = new Vector3(targetObject.localPosition.x, targetObject.localPosition.y +4, targetObject.position.z);
            transform.position = takipEdilecekPozisyon;
        }

        private void TargetObjectHealthControl()
        {
            if (_targetObjectAIController.enabled)
            {
                float healthUIValue = _targetObjectAIController.playerHealth / _targetObjectAIController.aiData.health;
                _ımage.fillAmount = healthUIValue;
                SpriteColorChange();
            }
           

        }

        private void SpriteColorChange()
        {
            
          
            
            
        }
    }
}
