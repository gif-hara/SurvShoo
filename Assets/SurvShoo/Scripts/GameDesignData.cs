using System;
using UnityEngine;

namespace SurvShoo
{
    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(menuName = "SurvShoo/GameDesignData")]
    public sealed class GameDesignData : ScriptableObject
    {
        [SerializeField]
        private _PlayerData playerData;
        public _PlayerData PlayerData => playerData;
        
        [Serializable]
        public class _PlayerData
        {
            [SerializeField]
            private Actor actorPrefab;
            public Actor ActorPrefab => actorPrefab;
            
            [SerializeField]
            private MinMaxValue moveSpeed;

            [SerializeField]
            private float slowModeMoveSpeedRate;
            
            [SerializeField]
            private int moveSpeedLevelMax;
            
            public float GetMoveSpeedRate(int level, bool isSlowMode)
            {
                var rate = (float)level / moveSpeedLevelMax;
                var slowModeRate = isSlowMode ? slowModeMoveSpeedRate : 1.0f;
                return moveSpeed.Evaluate(rate) * slowModeRate;
            }
        }
    }
}
