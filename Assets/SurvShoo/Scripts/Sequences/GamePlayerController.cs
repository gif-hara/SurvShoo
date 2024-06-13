using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using R3.Triggers;
using UnityEngine;
using UnitySequencerSystem;

namespace SurvShoo
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public sealed class GamePlayerController : ISequence
    {
        public UniTask PlayAsync(Container container, CancellationToken cancellationToken)
        {
            var actor = container.Resolve<Actor>("Owner");
            var inputController = TinyServiceLocator.Resolve<InputController>();
            var gameDesignData = TinyServiceLocator.Resolve<GameDesignData>();
            var gameInstanceData = TinyServiceLocator.Resolve<GameInstanceData>();
            var bulletFireController = new ActorBulletFireController(
                actor,
                gameDesignData.PlayerData.BulletSpawner,
                () => gameDesignData.PlayerData.GetFireCooldown(gameInstanceData.PlayerFireCooldownLevel.Data),
                () => $"BulletFirePointParent.{gameInstanceData.PlayerBulletFirePointLevel.Data}"
            );
            var currentOptionLevels = new List<int>();
            foreach (var _ in gameDesignData.OptionDataList)
            {
                currentOptionLevels.Add(0);
            }
            actor.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    var velocity = inputController.InputActions.Game.Move.ReadValue<Vector2>();
                    var moveSpeed = gameDesignData.PlayerData.GetMoveSpeedRate(gameInstanceData.PlayerMoveSpeedLevel.Data, inputController.InputActions.Game.SlowMode.IsPress());
                    actor.transform.localPosition += new Vector3(velocity.x, velocity.y, 0) * Time.deltaTime * moveSpeed;
                    bulletFireController.CanFire = inputController.InputActions.Game.Fire.IsPress();
                })
                .RegisterTo(actor.poolCancellationToken);
            for (var i = 0; i < gameInstanceData.OptionLevels.Count; i++)
            {
                var index = i;
                gameInstanceData.OptionLevels[index].DataAsObservable()
                    .Subscribe(x =>
                    {
                        var diff = x - currentOptionLevels[index];
                        if (diff > 0)
                        {
                            var oldParent = actor.LocatorHolder.Get($"OptionPoint.{index}.{currentOptionLevels[index]}");
                            for (var j = 0; j < oldParent.childCount; j++)
                            {
                                var child = oldParent.GetChild(j);
                                child.GetComponentInChildren<Actor>().ReturnToPool();
                            }
                            var optionGameDesignData = gameDesignData.OptionDataList[index];
                            var parent = actor.LocatorHolder.Get($"OptionPoint.{index}.{x}");
                            for (var j = 0; j < parent.childCount; j++)
                            {
                                var p = parent.GetChild(j);
                                var optionActor = optionGameDesignData.ActorSpawner.Spawn(p.position, p.rotation);
                                optionActor.transform.SetParent(p);
                            }
                        }
                        currentOptionLevels[index] = x;
                        // TODO: Optionが減る処理は必要になったら実装する
                    })
                    .RegisterTo(actor.poolCancellationToken);
            }
            return UniTask.CompletedTask;
        }
    }
}
