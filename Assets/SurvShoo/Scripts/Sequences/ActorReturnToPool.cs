using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using SurvShoo.Resolvers;
using UnityEngine;
using UnitySequencerSystem;

namespace SurvShoo
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public sealed class ActorReturnToPool : ISequence
    {
        [SubclassSelector]
        [SerializeReference]
        private ActorResolver actorResolver;

        public UniTask PlayAsync(Container container, CancellationToken cancellationToken)
        {
            var actor = actorResolver.Resolve(container);
            actor.ReturnToPool();
            return UniTask.CompletedTask;
        }
    }
}
