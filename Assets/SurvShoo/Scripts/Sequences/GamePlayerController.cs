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
            var bulletFireController = new ActorBulletFireController(
                actor,
                gameDesignData.PlayerData.BulletSpawner,
                () => gameDesignData.PlayerData.GetFireCooldown(gameInstanceData.PlayerFireCooldownLevel.Data),
                () => $"BulletFirePointParent.{gameInstanceData.PlayerBulletFirePointLevel.Data}"
            );
            actor.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    var velocity = inputController.InputActions.Game.Move.ReadValue<Vector2>();
                    var moveSpeed = gameDesignData.PlayerData.GetMoveSpeedRate(gameInstanceData.PlayerMoveSpeedLevel.Data, inputController.InputActions.Game.SlowMode.IsPress());
                    actor.transform.localPosition += new Vector3(velocity.x, velocity.y, 0) * Time.deltaTime * moveSpeed;
                    bulletFireController.CanFire = inputController.InputActions.Game.Fire.IsPress();
                })
                .RegisterTo(actor.poolCancellationToken);
            return UniTask.CompletedTask;
        }
    }
}
