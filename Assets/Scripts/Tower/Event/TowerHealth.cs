using System;
using Tower.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace Tower.Event
{
    public class TowerHealth : MonoBehaviour
    {
        private TowerController _towerController;
        public Image healthBarUI;
        
        // Start is called before the first frame update
        void Start()
        {
            _towerController = gameObject.transform.parent.GetComponent<TowerController>();
           
        }

        private void LateUpdate()
        {
            TargetObjectHealthControl();
        }
        
        private void TargetObjectHealthControl()
        {
            healthBarUI.fillAmount = _towerController.health / _towerController.towerData.health;

        }
      
    }
}
