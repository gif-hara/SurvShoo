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
        
        [SerializeField]
        private _BulletData.DictionaryList bulletData;
        public _BulletData.DictionaryList BulletData => bulletData;
        
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

        [Serializable]
        public class _BulletData
        {
            [SerializeField]
            private string id;
            public string Id => id;
            
            [SerializeField]
            private FloatData.DictionaryList database;
            public FloatData.DictionaryList Database => database;

            [Serializable]
            public class DictionaryList : DictionaryList<string, _BulletData>
            {
                public DictionaryList(Func<_BulletData, string> idSelector) : base(idSelector)
                {
                }
            }
        }

        [Serializable]
        public class FloatData
        {
            [SerializeField]
            private string id;
            public string Id => id;
            
            [SerializeField]
            private float data;
            public float Data => data;
            
            [Serializable]
            public class DictionaryList : DictionaryList<string, FloatData>
            {
                public DictionaryList(Func<FloatData, string> idSelector) : base(x => x.id)
                {
                }
            }
        }
    }
}
