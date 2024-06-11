using System.Threading;
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
            gameObject.SetActive(true);
        }

        public void OnPoolRelease()
        {
            gameObject.SetActive(false);
            poolCancellationTokenSource.Cancel();
            poolCancellationTokenSource.Dispose();
            poolCancellationTokenSource = null;
        }
    }
}
