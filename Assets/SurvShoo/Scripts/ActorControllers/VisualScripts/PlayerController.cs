using R3;
using R3.Triggers;
using Unity.VisualScripting;
using UnityEngine;

namespace SurvShoo.ActorControllers.VisualScripts
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class PlayerController : Unity.VisualScripting.Unit
    {
        [DoNotSerialize]
        public ControlInput input;
        
        [DoNotSerialize]
        public ControlOutput output;
        
        protected override void Definition()
        {
            input = ControlInput(nameof(input), flow =>
            {
                var actor = Variables.Object(flow.stack.gameObject).Get<Actor>("Owner");
                var inputController = TinyServiceLocator.Resolve<InputController>();
                var gameDesignData = TinyServiceLocator.Resolve<GameDesignData>();
                var gameInstanceData = TinyServiceLocator.Resolve<GameInstanceData>();
                var coolDownSeconds = gameDesignData.PlayerData.GetFireCooldown(gameInstanceData.PlayerFireCooldownLevel);
                actor.UpdateAsObservable()
                    .Subscribe(_ =>
                    {
                        var velocity = inputController.InputActions.Game.Move.ReadValue<Vector2>();
                        var moveSpeed = gameDesignData.PlayerData.GetMoveSpeedRate(
                            gameInstanceData.PlayerMoveSpeedLevel,
                            inputController.InputActions.Game.SlowMode.IsPress());
                        actor.transform.localPosition +=
                            new Vector3(velocity.x, velocity.y, 0) * Time.deltaTime * moveSpeed;
                        coolDownSeconds -= Time.deltaTime;
                
                        if (coolDownSeconds <= 0.0f && inputController.InputActions.Game.Fire.IsPress())
                        {
                            gameDesignData.PlayerData.BulletSpawner.Spawn(actor.transform.position,
                                Quaternion.identity);
                            coolDownSeconds =
                                gameDesignData.PlayerData.GetFireCooldown(gameInstanceData.PlayerFireCooldownLevel);
                        }
                    })
                    .RegisterTo(actor.poolCancellationToken);
                return output;
            });
            output = ControlOutput(nameof(output));
        }
    }
}
