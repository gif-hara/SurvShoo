using R3;
using R3.Triggers;
using UnityEngine;

namespace SurvShoo
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ActorPostureController
    {
        private Vector2 velocity;

        public void Setup(Actor actor)
        {
            actor.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    if (velocity == Vector2.zero)
                    {
                        return;
                    }

                    actor.transform.position += new Vector3(velocity.x, velocity.y, 0.0f);
                    velocity = Vector2.zero;
                })
                .RegisterTo(actor.poolCancellationToken);
        }

        public void Move(Vector2 velocity)
        {
            this.velocity += velocity;
        }
    }
}
