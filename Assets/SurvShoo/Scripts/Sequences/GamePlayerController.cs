using System;
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
            var coolDownSeconds = gameDesignData.PlayerData.GetFireCooldown(gameInstanceData.PlayerFireCooldownLevel);
            actor.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    var velocity = inputController.InputActions.Game.Move.ReadValue<Vector2>();
                    var moveSpeed = gameDesignData.PlayerData.GetMoveSpeedRate(gameInstanceData.PlayerMoveSpeedLevel, inputController.InputActions.Game.SlowMode.IsPress());
                    actor.transform.localPosition += new Vector3(velocity.x, velocity.y, 0) * Time.deltaTime * moveSpeed;
                    coolDownSeconds -= Time.deltaTime;

                    if (coolDownSeconds <= 0.0f && inputController.InputActions.Game.Fire.IsPress())
                    {
                        var firePointParent = actor.LocatorHolder.Get($"BulletFirePointParent.{gameInstanceData.PlayerBulletFirePointLevel}");
                        for(var i=0; i<firePointParent.childCount; i++)
                        {
                            var firePoint = firePointParent.GetChild(i);
                            gameDesignData.PlayerData.BulletSpawner.Spawn(firePoint.position, firePoint.rotation);
                        }
                        coolDownSeconds = gameDesignData.PlayerData.GetFireCooldown(gameInstanceData.PlayerFireCooldownLevel);
                    }
                })
                .RegisterTo(actor.poolCancellationToken);
            return UniTask.CompletedTask;
        }
    }
}
