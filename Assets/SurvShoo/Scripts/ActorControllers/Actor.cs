using System.Threading;
using LitMotion;
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
        private ActorLocatorHolder locatorHolder;
        public ActorLocatorHolder LocatorHolder => locatorHolder;

        [SerializeField]
        private int hitPoint;

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

        public Actor RentToPool()
        {
            var result = TinyServiceLocator.Resolve<ActorPool>().Rent(this);
            result.originalPrefab = this;
            return result;
        }

        public void ReturnToPool()
        {
            Assert.IsNotNull(originalPrefab);
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
