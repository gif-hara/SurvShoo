using System;
using System.Collections.Generic;
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
        private List<_OptionData> optionDataList;
        public List<_OptionData> OptionDataList => optionDataList;

        [SerializeField]
        private _BulletData.DictionaryList bulletData;
        public _BulletData.DictionaryList BulletData => bulletData;

        [Serializable]
        public class _PlayerData
        {
            [SerializeField]
            private ActorSpawner actorSpawner;
            public ActorSpawner ActorSpawner => actorSpawner;

            [SerializeField]
            private MinMaxValue moveSpeed;

            [SerializeField]
            private float slowModeMoveSpeedRate;

            [SerializeField]
            private int moveSpeedLevelMax;
            public int MoveSpeedLevelMax => moveSpeedLevelMax;

            [SerializeField]
            private ActorSpawner bulletSpawner;
            public ActorSpawner BulletSpawner => bulletSpawner;

            [SerializeField]
            private MinMaxValue fireCooldown;

            [SerializeField]
            private int fireCooldownLevelMax;
            public int FireCooldownLevelMax => fireCooldownLevelMax;

            [SerializeField]
            private int bulletFirePointLevelMax;
            public int BulletFirePointLevelMax => bulletFirePointLevelMax;

            public float GetMoveSpeedRate(int level, bool isSlowMode)
            {
                var rate = (float)level / moveSpeedLevelMax;
                var slowModeRate = isSlowMode ? slowModeMoveSpeedRate : 1.0f;
                return moveSpeed.Evaluate(rate) * slowModeRate;
            }

            public float GetFireCooldown(int level)
            {
                var rate = (float)level / fireCooldownLevelMax;
                return fireCooldown.Evaluate(rate);
            }
        }

        [Serializable]
        public class _OptionData
        {
            [SerializeField]
            private ActorSpawner actorSpawner;
            public ActorSpawner ActorSpawner => actorSpawner;

            [SerializeField]
            private ActorSpawner bulletSpawner;
            public ActorSpawner BulletSpawner => bulletSpawner;

            [SerializeField]
            private MinMaxValue fireCooldown;

            [SerializeField]
            private int fireCooldownLevelMax;

            [SerializeField]
            private int levelMax;
            public int LevelMax => levelMax;

            public float GetFireCooldown(int level)
            {
                var rate = (float)level / fireCooldownLevelMax;
                return fireCooldown.Evaluate(rate);
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
