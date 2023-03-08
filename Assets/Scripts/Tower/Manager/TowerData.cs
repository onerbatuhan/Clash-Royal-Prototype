using Unity.VisualScripting;
using UnityEngine;

namespace Tower.Manager
{
    [CreateAssetMenu(menuName = "ScriptableObjects/TowerData")]
    public class TowerData : ScriptableObject
    {
        public float health;
        public bool isBaseTower;
    }
}
