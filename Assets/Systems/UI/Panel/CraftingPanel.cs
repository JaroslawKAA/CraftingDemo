using System;
using Sirenix.OdinInspector;
using Systems.Core.GameEvents;
using Systems.Core.GameEvents.Events;
using Systems.Core.GameState;
using UnityEngine;
using UnityEngine.UI;

namespace Systems.UI.Panel
{
    public class CraftingPanel : UiPanel
    {
        // SERIALIZED
        [Title("Depend")]
        [SerializeField] [Required] RectTransform firstSlotRectTransform;
        [SerializeField] [Required] RectTransform secondSlotRectTransform;
        [SerializeField] [Required] RectTransform resultSlotRectTransform;
        [SerializeField] [Required] Button craftButton;

        // PRIVATE
        bool crafting;

        EventListener craftingCompletedListener;

        // UNITY EVENTS
        protected override void Awake()
        {
            base.Awake();
            SubscribeEvents();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            UnsubscribeEvents();
        }

        // METHODS
        protected override void TryShowPanel(Type state)
        {
            CachedGameObject.SetActive(state == typeof(CraftingGameState));
        }
        
        void SubscribeEvents()
        {
            craftButton.onClick.AddListener(Craft);

            craftingCompletedListener = new EventListener(OnCraftingCompleted);
            EventManager.RegisterListener<CraftingCompletedEvent>(craftingCompletedListener);
        }

        void UnsubscribeEvents()
        {
            craftButton.onClick.RemoveListener(Craft);

            EventManager.UnregisterListener<CraftingCompletedEvent>(craftingCompletedListener);
            craftingCompletedListener = null;
        }

        void Craft()
        {
            if (crafting) return;

            InventoryPanelRecord inventoryPanelRecord1 = firstSlotRectTransform.GetComponent<InventoryPanelRecord>();
            InventoryPanelRecord inventoryPanelRecord2 = secondSlotRectTransform.GetComponent<InventoryPanelRecord>();

            if (inventoryPanelRecord1 != null && inventoryPanelRecord2 != null)
            {
                EventManager.TriggerEvent(new CraftRequestEvent(inventoryPanelRecord1.ItemGuid, inventoryPanelRecord2.ItemGuid));
                Debug.Log("Crafting requested");
                crafting = true;
            }
        }

        void OnCraftingCompleted(EventBase eventBase)
        {
            Debug.Log("Crafting completed");
            CraftingCompletedEvent craftingCompletedEvent = eventBase as CraftingCompletedEvent;
            // TODO Consume items
            // TODO Add result to inventory
            crafting = false;
        }
    }
}