using R3;
using R3.Triggers;
using UnityEngine;

namespace SurvShoo
{
    /// <summary>
    /// 指定した<see cref="Actor"/>の移動方向から追尾するコンポーネント
    /// </summary>
    public sealed class ActorChaseFromDirection : MonoBehaviour
    {
        [SerializeField]
        private Actor actor;

        [SerializeField]
        private float smoothTime;

        [SerializeField]
        private float maxSpeed;

        void Awake()
        {
            var oldPosition = Vector3.zero;
            var velocity = 0.0f;
            actor.Events.OnPoolRent
                .Subscribe(_ =>
                {
                    oldPosition = actor.transform.position;
                    actor.UpdateAsObservable()
                        .Subscribe(__ =>
                        {
                            var nextPosition = actor.transform.position;
                            if (oldPosition != nextPosition)
                            {
                                var diff = nextPosition - oldPosition;
                                diff.z = -diff.y;
                                diff.y = 0;
                                var target = Quaternion.LookRotation(diff);
                                var smoothDamp = Mathf.SmoothDampAngle(
                                    transform.rotation.eulerAngles.z,
                                    target.eulerAngles.y,
                                    ref velocity,
                                    smoothTime,
                                    maxSpeed,
                                    Time.deltaTime
                                    );
                                transform.rotation = Quaternion.Euler(0.0f, 0.0f, smoothDamp);
                                oldPosition = nextPosition;
                            }
                        })
                        .RegisterTo(actor.poolCancellationToken);
                })
                .RegisterTo(this.destroyCancellationToken);
        }
    }
}
