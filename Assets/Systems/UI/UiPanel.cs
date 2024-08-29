using Sirenix.OdinInspector;
using UnityEngine;

namespace Systems.UI
{
    public class UiPanel : MonoBehaviour
    {
        [Title("Ui Panel")]
        [SerializeField] bool disableAfterStart = true;
    
        protected GameObject CachedGameObject;

        protected virtual void Awake()
        {
            CachedGameObject = gameObject;
        }

        protected virtual void Start()
        {
            if (disableAfterStart) 
                CachedGameObject.SetActive(false);
        }
    }
}
