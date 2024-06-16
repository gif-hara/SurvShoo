using System.Collections.Generic;
using R3;
using R3.Triggers;
using UnityEngine;

namespace SurvShoo
{
    /// <summary>
    /// 指定した<see cref="Actor"/>の座標を追尾するコンポーネント
    /// </summary>
    public sealed class ActorChaseFromPosition : MonoBehaviour
    {
        [SerializeField]
        private Actor actor;

        [SerializeField]
        private int waitCount;

        private List<Vector3> positions = new();

        void Awake()
        {
            actor.Events.OnPoolRent
                .Subscribe(_ =>
                {
                    positions.Add(actor.transform.position);
                    actor.UpdateAsObservable()
                        .Subscribe(__ =>
                        {
                            var prevPosition = positions[^1];
                            var nextPosition = actor.transform.position;
                            if (prevPosition != nextPosition)
                            {
                                positions.Add(nextPosition);
                                transform.position = positions[0];
                                if (positions.Count > waitCount)
                                {
                                    positions.RemoveAt(0);
                                }
                            }
                        })
                        .RegisterTo(actor.poolCancellationToken);
                })
                .RegisterTo(this.destroyCancellationToken);
            actor.Events.OnPoolReturn
                .Subscribe(_ =>
                {
                    positions.Clear();
                })
                .RegisterTo(this.destroyCancellationToken);
        }
    }
}
