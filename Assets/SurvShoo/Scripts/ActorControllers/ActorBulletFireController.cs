using System;
using System.Threading;
using R3;
using R3.Triggers;
using UnityEngine;

namespace SurvShoo
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ActorBulletFireController
    {
        public static void Attach(
            Actor actor,
            ActorSpawner bulletSpawner,
            Func<float> cooldownTimeProvider,
            Func<Transform> firePointProvider,
            CancellationToken scope
        )
        {
            var currentCooldownTime = 0.0f;
            actor.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    currentCooldownTime -= Time.deltaTime;
                    if (currentCooldownTime <= 0.0f && actor.Events.CanFire.Value)
                    {
                        var firePointParent = firePointProvider();
                        for (var i = 0; i < firePointParent.childCount; i++)
                        {
                            var firePoint = firePointParent.GetChild(i);
                            bulletSpawner.Spawn(firePoint.position, firePoint.rotation);
                        }

                        currentCooldownTime = cooldownTimeProvider();
                    }
                })
                .RegisterTo(scope);
        }
    }
}
