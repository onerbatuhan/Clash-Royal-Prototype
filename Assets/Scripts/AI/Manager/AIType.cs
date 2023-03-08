using UnityEngine;

namespace AI.Manager
{
    public class AIType : MonoBehaviour
    {
       public enum AIPlayerType
       {
           Melee,
           Aerial,
           Range,
           Support,
           Tower
       }
       public enum AIPlayerMotionType
       {
           Idle,
           Run,
           Attack,
           Dead
       }

       public enum AIAttackType
       {
           Single,
           Area
       }

      
    }
}
