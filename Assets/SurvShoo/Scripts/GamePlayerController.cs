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
            var gameDesign = TinyServiceLocator.Resolve<GameDesign>();
            actor.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    var velocity = inputController.InputActions.Game.Move.ReadValue<Vector2>();
                    var moveSpeed = gameDesign.PlayerData.MoveSpeed.Evaluate(0.0f);
                    var moveSpeedRate = inputController.InputActions.Game.SlowMode.IsPress()
                        ? gameDesign.PlayerData.SlowModeMoveSpeedRate
                        : 1.0f;
                    actor.transform.localPosition += new Vector3(velocity.x, velocity.y, 0) * Time.deltaTime * moveSpeed * moveSpeedRate;

                    if (inputController.InputActions.Game.Fire.IsPress())
                    {
                        Debug.Log("Fire");
                    }
                })
                .RegisterTo(actor.destroyCancellationToken);
        }
    }
}
