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
        private IntInstanceData playerMoveSpeedLevel;
        public IntInstanceData PlayerMoveSpeedLevel => playerMoveSpeedLevel;
        
        [SerializeField]
        private IntInstanceData playerFireCooldownLevel;
        public IntInstanceData PlayerFireCooldownLevel => playerFireCooldownLevel;
        
        [SerializeField]
        private IntInstanceData playerBulletFirePointLevel;
        public IntInstanceData PlayerBulletFirePointLevel => playerBulletFirePointLevel;
        
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
