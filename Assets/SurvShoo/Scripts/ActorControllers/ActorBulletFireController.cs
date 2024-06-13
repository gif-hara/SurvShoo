using System;
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
        public bool CanFire { get; set; }
        
        private float currentCooldownTime = 0.0f;
        
        public ActorBulletFireController(
            Actor actor,
            ActorSpawner bulletSpawner,
            Func<float> cooldownTimeProvider,
            Func<string> firePointIdProvider
        )
        {
            currentCooldownTime = 0.0f;
            actor.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    currentCooldownTime -= Time.deltaTime;
                    if (currentCooldownTime <= 0.0f && CanFire)
                    {
                        var firePointParent = actor.LocatorHolder.Get(firePointIdProvider());
                        for (var i = 0; i < firePointParent.childCount; i++)
                        {
                            var firePoint = firePointParent.GetChild(i);
                            bulletSpawner.Spawn(firePoint.position, firePoint.rotation);
                        }

                        currentCooldownTime = cooldownTimeProvider();
                    }
                })
                .RegisterTo(actor.poolCancellationToken);
        }
    }
}
