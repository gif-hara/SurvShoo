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
            actor.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    var velocity = inputController.InputActions.Game.Move.ReadValue<Vector2>();
                    actor.transform.position += new Vector3(velocity.x, velocity.y, 0) * Time.deltaTime * 100.0f;
                })
                .RegisterTo(actor.destroyCancellationToken);
        }
    }
}
