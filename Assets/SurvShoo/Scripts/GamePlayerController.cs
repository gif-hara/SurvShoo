using R3;
using R3.Triggers;
using UnityEngine;

namespace SurvShoo
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class GamePlayerController
    {
        public void Setup(Actor actor)
        {
            var inputController = TinyServiceLocator.Resolve<InputController>();
            var gameDesignData = TinyServiceLocator.Resolve<GameDesignData>();
            var gameInstanceData = TinyServiceLocator.Resolve<GameInstanceData>();
            actor.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    var velocity = inputController.InputActions.Game.Move.ReadValue<Vector2>();
                    var moveSpeed = gameDesignData.PlayerData.GetMoveSpeedRate(gameInstanceData.PlayerMoveSpeedLevel, inputController.InputActions.Game.SlowMode.IsPress());
                    actor.transform.localPosition += new Vector3(velocity.x, velocity.y, 0) * Time.deltaTime * moveSpeed;

                    if (inputController.InputActions.Game.Fire.IsPress())
                    {
                        Debug.Log("Fire");
                    }
                })
                .RegisterTo(actor.destroyCancellationToken);
        }
    }
}