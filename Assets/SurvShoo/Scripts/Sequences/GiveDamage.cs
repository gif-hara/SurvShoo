using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using R3.Triggers;
using SurvShoo.Resolvers;
using UnityEngine;
using UnitySequencerSystem;
using UnitySequencerSystem.Resolvers;

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
        
        [SubclassSelector]
        [SerializeReference]
        private IntResolver damageResolver;

        public UniTask PlayAsync(Container container, CancellationToken cancellationToken)
        {
            var actor = actorResolver.Resolve(container);
            var damage = damageResolver.Resolve(container);
            actor.OnTriggerEnter2DAsObservable()
                .Subscribe(collider =>
                {
                    var targetActor = collider.attachedRigidbody.GetComponent<Actor>();
                    if (targetActor == null)
                    {
                        return;
                    }
                    targetActor.TakeDamage(damage);
                })
                .RegisterTo(cancellationToken);
            return UniTask.CompletedTask;
        }
    }
}
