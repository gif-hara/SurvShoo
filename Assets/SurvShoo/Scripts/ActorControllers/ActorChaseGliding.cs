using System.Collections.Generic;
using R3;
using R3.Triggers;
using UnityEngine;

namespace SurvShoo
{
    /// <summary>
    /// 指定した<see cref="Actor"/>の座標を滑るように追尾するコンポーネント
    /// </summary>
    public sealed class ActorChaseGliding : MonoBehaviour
    {
        [SerializeField]
        private Actor actor;

        [SerializeField]
        private Vector3 offset;
        
        [SerializeField]
        private float acceleration;
        
        [SerializeField]
        private float minSpeed;
        
        [SerializeField]
        private float maxSpeed;
        
        [SerializeField]
        private float rotationSpeed;

        void Awake()
        {
            var velocity = 0.0f;
            var direction = Quaternion.identity;
            actor.Events.OnPoolRent
                .Subscribe(_ =>
                {
                    velocity = Random.Range(0.0f, maxSpeed);
                    var randomDirection = Random.onUnitSphere;
                    randomDirection.z = 0;
                    randomDirection.Normalize();
                    direction = Quaternion.LookRotation(randomDirection, Vector3.forward);
                    actor.UpdateAsObservable()
                        .Subscribe(__ =>
                        {
                            var targetPosition = actor.transform.position + offset;
                            var lookAt = Quaternion.LookRotation(targetPosition - transform.position, Vector3.forward);
                            direction = Quaternion.Lerp(direction, lookAt, rotationSpeed * Time.deltaTime);
                            velocity += Quaternion.Dot(direction, lookAt) * acceleration * Time.deltaTime;
                            velocity = Mathf.Clamp(velocity, minSpeed, maxSpeed);
                            Debug.Log($"direction: {direction}, lookAt: {lookAt}, velocity: {velocity} Dot: {Quaternion.Dot(direction, lookAt)}");
                            transform.position += direction * Vector3.up * velocity * Time.deltaTime;
                        })
                        .RegisterTo(actor.poolCancellationToken);
                })
                .RegisterTo(this.destroyCancellationToken);
            actor.Events.OnPoolReturn
                .Subscribe(_ =>
                {
                })
                .RegisterTo(this.destroyCancellationToken);
        }
    }
}
