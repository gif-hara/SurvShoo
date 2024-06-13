using System;
using Cysharp.Threading.Tasks;
using R3;
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
        
        private ReactiveProperty<int> playerMoveSpeedLevelReactiveProperty;

        public int PlayerMoveSpeedLevel
        {
            get => playerMoveSpeedLevel;
            set
            {
                playerMoveSpeedLevel = value;
                playerMoveSpeedLevelReactiveProperty ??= new ReactiveProperty<int>();
                playerMoveSpeedLevelReactiveProperty.Value = value;
            }
        }

        public ReadOnlyReactiveProperty<int> PlayerMoveSpeedLevelAsObservable()
        {
            playerMoveSpeedLevelReactiveProperty ??= new ReactiveProperty<int>(playerMoveSpeedLevel);
            return playerMoveSpeedLevelReactiveProperty;
        }

        [SerializeField]
        private int playerFireCooldownLevel;
        
        private ReactiveProperty<int> playerFireCooldownLevelReactiveProperty;

        public int PlayerFireCooldownLevel
        {
            get => playerFireCooldownLevel;
            set
            {
                playerFireCooldownLevel = value;
                playerFireCooldownLevelReactiveProperty ??= new ReactiveProperty<int>();
                playerFireCooldownLevelReactiveProperty.Value = value;
            }
        }

        public ReadOnlyReactiveProperty<int> PlayerFireCooldownLevelAsObservable()
        {
            playerFireCooldownLevelReactiveProperty ??= new ReactiveProperty<int>(playerFireCooldownLevel);
            return playerFireCooldownLevelReactiveProperty;
        }

        [SerializeField]
        private int playerBulletFirePointLevel;
        
        private ReactiveProperty<int> playerBulletFirePointLevelReactiveProperty;

        public int PlayerBulletFirePointLevel
        {
            get => playerBulletFirePointLevel;
            set
            {
                playerBulletFirePointLevel = value;
                playerBulletFirePointLevelReactiveProperty ??= new ReactiveProperty<int>();
                playerBulletFirePointLevelReactiveProperty.Value = value;
            }
        }

        public ReadOnlyReactiveProperty<int> PlayerBulletFirePointLevelAsObservable()
        {
            playerBulletFirePointLevelReactiveProperty ??= new ReactiveProperty<int>(playerBulletFirePointLevel);
            return playerBulletFirePointLevelReactiveProperty;
        }
    }
}
