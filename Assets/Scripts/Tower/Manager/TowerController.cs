using System;
using UnityEngine;

namespace Tower.Manager
{
    public class TowerController : MonoBehaviour
    {
        public TowerData towerData;
        public float health;
        public bool isDead;

        private void Start()
        {
            health = towerData.health;
        }

        private void LateUpdate()
        {
            if (health <= 0 && !isDead)
            {
                isDead = true;
            }
        }
    }
}
