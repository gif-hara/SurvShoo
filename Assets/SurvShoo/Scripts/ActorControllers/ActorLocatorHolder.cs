using System.Collections.Generic;
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

        [SerializeField]
        private List<Transform> bulletFirePoints;
        public List<Transform> BulletFirePoints => bulletFirePoints;
    }
}
