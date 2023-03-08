using System;
using Game;
using Teams.Manager;
using Tower.Manager;
using UnityEngine;

namespace Tower.Event
{
    public class TowerDestroyEvent : MonoBehaviour
    {
        private TowerController _towerController;
        public bool isDestroy;
        private void Start()
        {
            _towerController = gameObject.GetComponent<TowerController>();
        }

        private void LateUpdate()
        {
            if (_towerController.health <= 0 && !isDestroy)
            {
                if (_towerController.towerData.isBaseTower)
                {
                    GameFinishEvent.Instance.GameStateFilter();
                }
                isDestroy = true;
                TeamController.Instance.allPlayer.Remove(gameObject);
                Destroy(gameObject);
            }
        }
    }
}
