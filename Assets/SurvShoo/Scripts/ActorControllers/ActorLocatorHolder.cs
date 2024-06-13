using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace SurvShoo
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ActorLocatorHolder : MonoBehaviour
    {
        [SerializeField]
        private List<Element> elements;
        
        private Dictionary<string, Transform> cachedLocators = null;

        public Transform Get(string key)
        {
            if(cachedLocators == null)
            {
                cachedLocators = new Dictionary<string, Transform>();
                foreach (var element in elements)
                {
                    cachedLocators[element.Key] = element.Locator;
                }
            }
            
            Assert.IsTrue(cachedLocators.ContainsKey(key), $"Key {key} not found in {name}");
            return cachedLocators[key];
        }

        [Serializable]
        public class Element
        {
            [SerializeField]
            private string key;
            public string Key => key;
            
            [SerializeField]
            private Transform locator;
            public Transform Locator => locator;
        }
    }
}
