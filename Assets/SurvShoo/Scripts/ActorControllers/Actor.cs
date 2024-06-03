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
        private CancellationTokenSource poolCancellationTokenSource;

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
            return TinyServiceLocator.Resolve<ActorPool>().Rent(this);
        }

        public void ReturnToPool()
        {
            TinyServiceLocator.Resolve<ActorPool>().Return(this);
        }

        public void OnPoolRent()
        {
            poolCancellationTokenSource = new CancellationTokenSource();
        }

        public void OnPoolRelease()
        {
            poolCancellationTokenSource.Cancel();
            poolCancellationTokenSource.Dispose();
            poolCancellationTokenSource = null;
        }
    }
}
