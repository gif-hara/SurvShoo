using Cysharp.Threading.Tasks;
using UnityEngine;
using UnitySequencerSystem;

namespace SurvShoo
{
    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(menuName = "SurvShoo/ActorSpawner")]
    public sealed class ActorSpawner : ScriptableObject
    {
        [SerializeField]
        private Actor actorPrefab;

        [SerializeField]
        private ScriptableSequences onEnterSequences;

        public void Spawn(Vector3 position, Quaternion rotation)
        {
            var actor = actorPrefab.RentToPool();
            actor.transform.position = position;
            actor.transform.rotation = rotation;
            var container = new Container();
            container.Register("Owner", actor);
            container.Register("Owner", actor.transform);
            container.Register("View", actor.LocatorHolder.View);
            var sequencer = new Sequencer(container, onEnterSequences.Sequences);
            sequencer.PlayAsync(actor.poolCancellationToken).Forget();
        }
    }
}
