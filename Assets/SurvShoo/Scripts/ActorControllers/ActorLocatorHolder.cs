using UnityEngine;

namespace SurvShoo
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ActorLocatorHolder : MonoBehaviour
    {
        [SerializeField]
        private Transform view;
        public Transform View => view;
    }
}
