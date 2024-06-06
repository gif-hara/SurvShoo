using System;
using UnityEngine;
using UnitySequencerSystem;
using UnitySequencerSystem.Resolvers;

namespace SurvShoo.Resolvers
{
    public abstract class ActorResolver : IResolver<Actor>
    {
        public abstract Actor Resolve(Container container);

        [AddTypeMenu("Reference")]
        [Serializable]
        public sealed class Reference : ActorResolver
        {
            [SerializeField]
            private Actor target;

            public Reference()
            {
            }

            public Reference(Actor target)
            {
                this.target = target;
            }

            public override Actor Resolve(Container container)
            {
                return target;
            }
        }

        [AddTypeMenu("Name")]
        [Serializable]
        public sealed class Name : ActorResolver
        {
            [SerializeField]
            private string name;

            public Name()
            {
            }

            public Name(string name)
            {
                this.name = name;
            }

            public override Actor Resolve(Container container)
            {
                return container.Resolve<Actor>(name);
            }
        }
    }
}
