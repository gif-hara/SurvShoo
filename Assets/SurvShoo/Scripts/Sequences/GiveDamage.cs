using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using R3.Triggers;
using SurvShoo.Resolvers;
using UnityEngine;
using UnitySequencerSystem;

namespace SurvShoo
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public sealed class GiveDamage : ISequence
    {
        [SubclassSelector]
        [SerializeReference]
        private ActorResolver actorResolver;

        public UniTask PlayAsync(Container container, CancellationToken cancellationToken)
        {
            var actor = actorResolver.Resolve(container);
            actor.OnTriggerEnter2DAsObservable()
                .Subscribe(collider =>
                {
                    Debug.Log($"Hit {collider.attachedRigidbody.name}");
                })
                .AddTo(cancellationToken)
                .AddTo(actor.poolCancellationToken);
            return UniTask.CompletedTask;
        }
    }
}
