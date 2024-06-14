using System.Collections.Generic;
using R3;
using R3.Triggers;
using UnityEngine;

namespace SurvShoo
{
    /// <summary>
    /// <see cref="Define.ActorType"/>からランダムな<see cref="Actor"/>を追尾するコンポーネント
    /// </summary>
    public sealed class ActorChaseFromActorType : MonoBehaviour
    {
        [SerializeField]
        private Actor actor;
        
        [SerializeField]
        private Define.ActorType targetActorType;

        [SerializeField]
        private Vector3 defaultPosition;

        [SerializeField]
        private float smoothTime;

        [SerializeField]
        private float maxSpeed;
        
        void Awake()
        {
            var targets = new Queue<Actor>();
            var velocity = Vector3.zero;
            this.OnTriggerEnter2DAsObservable()
                .Subscribe(x =>
                {
                    var target = x.attachedRigidbody.TryGetComponent<Actor>(out var a) ? a : null;
                    if(target != null && target.ActorType == targetActorType)
                    {
                        targets.Enqueue(target);
                    }
                })
                .RegisterTo(destroyCancellationToken);
            actor.Events.OnPoolRent
                .Subscribe(_ =>
                {
                    actor.UpdateAsObservable()
                        .Subscribe(__ =>
                        {
                            var toPosition = actor.transform.position + defaultPosition;
                            if (targets.Count > 0)
                            {
                                while (targets.Count > 0 && !targets.Peek().isActiveAndEnabled)
                                {
                                    targets.Dequeue();
                                }

                                if (targets.Count > 0)
                                {
                                    toPosition = targets.Peek().transform.position;
                                }
                            }
                            transform.position = Vector3.SmoothDamp(
                                transform.position,
                                toPosition,
                                ref velocity,
                                smoothTime,
                                maxSpeed,
                                Time.deltaTime
                            );
                        })
                        .RegisterTo(actor.poolCancellationToken);
                })
                .RegisterTo(destroyCancellationToken);
            actor.Events.OnPoolReturn
                .Subscribe(_ =>
                {
                    targets.Clear();
                    velocity = Vector3.zero;
                })
                .RegisterTo(destroyCancellationToken);
        }
    }
}
