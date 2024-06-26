using System.Threading;
using R3;
using UnityEngine;
using UnityEngine.Assertions;

namespace SurvShoo
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Actor : MonoBehaviour
    {
        [SerializeField]
        private Define.ActorType actorType;
        public Define.ActorType ActorType => actorType;
        
        [SerializeField]
        private ActorLocatorHolder locatorHolder;
        public ActorLocatorHolder LocatorHolder => locatorHolder;

        public ActorEvents Events { get; } = new();

        [SerializeField]
        private int hitPoint;
        
        [SerializeField]
        private float radius;
        public float Radius => radius;

        private int currentHitPoint;
        
        private CancellationTokenSource poolCancellationTokenSource;

        private Actor originalPrefab;
        
        public CancellationToken poolCancellationToken
        {
            get
            {
                Assert.IsNotNull(poolCancellationTokenSource);
                return poolCancellationTokenSource.Token;
            }
        }

        public Actor RentToPool(Vector3 position, Quaternion rotation)
        {
            var result = TinyServiceLocator.Resolve<ActorPool>().Rent(this);
            var t = result.transform;
            t.position = position;
            t.rotation = rotation;
            TinyServiceLocator.Resolve<ActorManager>().Add(result);
            result.originalPrefab = this;
            result.Events.OnPoolRent.OnNext(Unit.Default);
            return result;
        }

        public void ReturnToPool()
        {
            Assert.IsNotNull(originalPrefab);
            Events.OnPoolReturn.OnNext(Unit.Default);
            TinyServiceLocator.Resolve<ActorPool>().Return(originalPrefab, this);
        }

        public void OnPoolRent()
        {
            poolCancellationTokenSource = new CancellationTokenSource();
            currentHitPoint = hitPoint;
            gameObject.SetActive(true);
        }

        public void OnPoolRelease()
        {
            gameObject.SetActive(false);
            poolCancellationTokenSource.Cancel();
            poolCancellationTokenSource.Dispose();
            poolCancellationTokenSource = null;
        }

        public void TakeDamage(int damage)
        {
            if(currentHitPoint <= 0)
            {
                return;
            }
            
            currentHitPoint -= damage;
            if(currentHitPoint <= 0)
            {
                currentHitPoint = 0;
                ReturnToPool();
            }
        }
    }
}
