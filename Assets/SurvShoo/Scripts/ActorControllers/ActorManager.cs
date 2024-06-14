using System.Collections.Generic;

namespace SurvShoo
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ActorManager
    {
        public readonly List<Actor> Enemies = new();

        public void Add(Actor actor)
        {
            if (actor.ActorType == Define.ActorType.Enemy)
            {
                Enemies.Add(actor);
            }
        }
    }
}
