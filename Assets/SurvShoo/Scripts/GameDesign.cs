using System;
using UnityEngine;

namespace SurvShoo
{
    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(menuName = "SurvShoo/GameDesign")]
    public sealed class GameDesign : ScriptableObject
    {
        [SerializeField]
        private _PlayerData playerData;
        public _PlayerData PlayerData => playerData;
        
        [Serializable]
        public class _PlayerData
        {
            [SerializeField]
            private MinMaxValue moveSpeed;
            public MinMaxValue MoveSpeed => moveSpeed;

            [SerializeField]
            private float slowModeMoveSpeedRate;
            public float SlowModeMoveSpeedRate => slowModeMoveSpeedRate;
        }
    }
}
