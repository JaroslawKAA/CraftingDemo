using System;
using Sirenix.OdinInspector;
using Systems.Core.GameState;
using UnityEngine;
using UnityEngine.UI;

namespace Systems.UI.Panel
{
    public class CraftingPanel : UiPanel
    {
        [Title("Depend")]
        [SerializeField] [Required] RectTransform firstSlotRectTransform;
        [SerializeField] [Required] RectTransform secondSlotRectTransform;
        [SerializeField] [Required] RectTransform resultSlotRectTransform;
        [SerializeField] [Required] Button craftButton;

        protected override void Awake()
        {
            base.Awake();
            craftButton.onClick.AddListener(Craft);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            craftButton.onClick.RemoveListener(Craft);
        }

        protected override void TryShowPanel(Type state)
        {
            CachedGameObject.SetActive(state == typeof(CraftingGameState));
        }

        void Craft()
        {
            throw new NotImplementedException();
        }
    }
}
