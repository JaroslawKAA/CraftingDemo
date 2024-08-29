using System;
using Sirenix.OdinInspector;
using Systems.Core;
using UnityEngine;

namespace Systems.UI.Panel
{
    public abstract class UiPanel : MonoBehaviour
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
            
            GameManager.Instance.onGameStateChanged += TryShowPanel;
        }

        protected virtual void OnDestroy()
        {
            GameManager.Instance.onGameStateChanged -= TryShowPanel;
        }

        protected abstract void TryShowPanel(Type state);
    }
}
