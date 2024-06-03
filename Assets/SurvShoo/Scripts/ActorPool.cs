using System.Collections.Generic;
using UnityEngine.Pool;

namespace SurvShoo
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ActorPool
    {
        private readonly Dictionary<Actor, Pool> pools = new();

        public ActorPool()
        {
        }

        public Actor Rent(Actor prefab)
        {
            if (!pools.TryGetValue(prefab, out var pool))
            {
                pool = new Pool(prefab);
                pools.Add(prefab, pool);
            }

            return pool.Rent();
        }

        public void Return(Actor actor)
        {
            if (pools.TryGetValue(actor, out var pool))
            {
                pool.Return(actor);
            }
        }

        public class Pool
        {
            private readonly ObjectPool<Actor> pool;

            private readonly Actor prefab;

            public Pool(Actor prefab)
            {
                this.prefab = prefab;
                pool = new ObjectPool<Actor>(OnCreate);
            }

            public Actor Rent()
            {
                return pool.Get();
            }

            public void Return(Actor actor)
            {
                pool.Release(actor);
            }

            private Actor OnCreate()
            {
                return UnityEngine.Object.Instantiate(prefab);
            }
        }
    }
}
