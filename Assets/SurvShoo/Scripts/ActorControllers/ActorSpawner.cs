using UnityEngine;

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

        public void Spawn(Vector3 position, Quaternion rotation)
        {
            var actor = actorPrefab.RentToPool();
            var t = actor.transform;
            t.position = position;
            t.rotation = rotation;
        }
    }
}
