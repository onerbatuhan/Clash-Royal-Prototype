using DesignPattern;
using Team.ScriptTable;
using UnityEngine;

namespace Player
{
    public class PlayerController : Singleton<PlayerController>
    {
        public TeamData playerTeamData;
    }
}
