using UnityEngine;

namespace LineFlow.Common
{
    /// <summary>
    /// This will be the parent class for all Unity singleton objects.
    /// </summary>
    /// <typeparam name="T">Type of the object</typeparam>
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        
        protected virtual void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            } else {
                _instance = this as T;
            }
        }

        public static T Instance => _instance;
    }
}