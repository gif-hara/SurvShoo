using R3;
using UnityEngine;

namespace SurvShoo
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ActorSetParent : MonoBehaviour
    {
        [SerializeField]
        private Actor actor;

        [SerializeField]
        private Transform targetParent;
        
        private void Awake()
        {
            var cachedParent = transform.parent;
            actor.Events.OnPoolRent
                .Subscribe(_ =>
                {
                    transform.SetParent(targetParent);
                })
                .RegisterTo(destroyCancellationToken);
            actor.Events.OnPoolReturn
                .Subscribe(_ =>
                {
                    transform.SetParent(cachedParent);
                })
                .RegisterTo(destroyCancellationToken);

        }
    }
}
