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
        [Title("Debug")]
        [SerializeField] bool crafting;

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

            InventoryPanelRecord inventoryPanelRecord1 = firstSlotRectTransform.GetComponentInChildren<InventoryPanelRecord>();
            InventoryPanelRecord inventoryPanelRecord2 = secondSlotRectTransform.GetComponentInChildren<InventoryPanelRecord>();

            if (inventoryPanelRecord1 != null && inventoryPanelRecord2 != null)
            {
                crafting = true;
                EventManager.TriggerEvent(new CraftRequestEvent(inventoryPanelRecord1.ItemGuid, inventoryPanelRecord2.ItemGuid));
                Debug.Log("Crafting requested");
            }
        }

        void OnCraftingCompleted(EventBase eventBase)
        {
            Debug.Log("Crafting completed");
            CraftingCompletedEvent craftingCompletedEvent = eventBase as CraftingCompletedEvent;
            
            // DestroyRecordsInSlots();
            EventManager.TriggerEvent(new RefreshInventoryRequestEvent());
            crafting = false;
        }

        void DestroyRecordsInSlots()
        {
            InventoryPanelRecord inventoryPanelRecord1 = firstSlotRectTransform.GetComponentInChildren<InventoryPanelRecord>();
            InventoryPanelRecord inventoryPanelRecord2 = secondSlotRectTransform.GetComponentInChildren<InventoryPanelRecord>();
            Destroy(inventoryPanelRecord1.CachedGameObject);
            Destroy(inventoryPanelRecord2.CachedGameObject);
        }
    }
}