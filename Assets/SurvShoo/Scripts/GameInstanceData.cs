using System;
using System.Collections.Generic;
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
        private IntInstanceData playerMoveSpeedLevel;
        public IntInstanceData PlayerMoveSpeedLevel => playerMoveSpeedLevel;

        [SerializeField]
        private IntInstanceData playerFireCooldownLevel;
        public IntInstanceData PlayerFireCooldownLevel => playerFireCooldownLevel;

        [SerializeField]
        private IntInstanceData playerBulletFirePointLevel;
        public IntInstanceData PlayerBulletFirePointLevel => playerBulletFirePointLevel;

        [SerializeField]
        private List<IntInstanceData> optionLevels;
        public List<IntInstanceData> OptionLevels => optionLevels;

        [SerializeField]
        private IntInstanceData optionCooldownLevel;
        public IntInstanceData OptionCooldownLevel => optionCooldownLevel;

        public void AddPlayerMoveSpeedLevel(int value)
        {
            playerMoveSpeedLevel.Data = Mathf.Clamp(
                playerMoveSpeedLevel.Data + value,
                0,
                TinyServiceLocator.Resolve<GameDesignData>().PlayerData.MoveSpeedLevelMax
                );
        }

        public void AddPlayerFireCooldownLevel(int value)
        {
            playerFireCooldownLevel.Data = Mathf.Clamp(
                playerFireCooldownLevel.Data + value,
                0,
                TinyServiceLocator.Resolve<GameDesignData>().PlayerData.FireCooldownLevelMax
                );
        }

        public void AddPlayerBulletFirePointLevel(int value)
        {
            playerBulletFirePointLevel.Data = Mathf.Clamp(
                playerBulletFirePointLevel.Data + value,
                0,
                TinyServiceLocator.Resolve<GameDesignData>().PlayerData.BulletFirePointLevelMax
                );
        }

        public void AddOptionLevel(int index, int value)
        {
            optionLevels[index].Data = Mathf.Clamp(
                optionLevels[index].Data + value,
                0,
                TinyServiceLocator.Resolve<GameDesignData>().OptionDataList[index].LevelMax
                );
        }

        [Serializable]
        public abstract class InstanceData<T>
        {
            [SerializeField]
            private T data;

            private ReactiveProperty<T> dataReactiveProperty;

            public T Data
            {
                get => data;
                set
                {
                    data = value;
                    dataReactiveProperty ??= new ReactiveProperty<T>(data);
                    dataReactiveProperty.Value = value;
                }
            }

            public ReadOnlyReactiveProperty<T> DataAsObservable()
            {
                dataReactiveProperty ??= new ReactiveProperty<T>(data);
                return dataReactiveProperty;
            }
        }

        [Serializable]
        public sealed class IntInstanceData : InstanceData<int> { }
    }
}
