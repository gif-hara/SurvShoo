using System;
using UnityEngine;

namespace SurvShoo
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public sealed class GameInstanceData
    {
        [SerializeField]
        private int playerMoveSpeedLevel;
        public int PlayerMoveSpeedLevel { get => playerMoveSpeedLevel; set => playerMoveSpeedLevel = value; }

        [SerializeField]
        private int playerFireCooldownLevel;
        public int PlayerFireCooldownLevel
        {
            get => playerFireCooldownLevel; set => playerFireCooldownLevel = value;
        }
    }
}
