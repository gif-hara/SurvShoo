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

        private CancellationToken poolCancellationToken
        {
            get
            {
                Assert.IsNotNull(poolCancellationTokenSource);
                return poolCancellationTokenSource.Token;
            }
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
