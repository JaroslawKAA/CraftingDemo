using UnityEngine;

namespace Systems.Core.Patterns.Singleton
{
    public class SimpleSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance;

        protected virtual void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError($"Duplicated singleton - {gameObject.name} removed");
                Destroy(gameObject);
            }
            else
            {
                Instance = this as T;
            }
        }
    }
}