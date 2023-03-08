using AI.Manager;
using UnityEngine;

namespace AI.ScriptTable
{
    [CreateAssetMenu(menuName = "ScriptableObjects/AIEnemyData")]
    public class AIData : ScriptableObject
    {
        public string characterName;
        public GameObject characterObject;
        public float speed;
        public float health;
        public float damage;
        public float areaDamage;
        public int stamina;
        public float attackRange;
        public float attackSpeed;
        public float supportHealth;
        public float spawnChance;
        public AIType.AIPlayerType playerType;
        public AIType.AIAttackType playerAttackType;
        public GameObject healthBarObjectSprite;
    }
}
