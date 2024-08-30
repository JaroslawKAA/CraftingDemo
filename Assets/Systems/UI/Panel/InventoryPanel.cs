using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Systems.Core.GameEvents;
using Systems.Core.GameEvents.Events;
using Systems.Core.GameState;
using Systems.Core.Services;
using UnityEngine;

namespace Systems.UI.Panel
{
    public class InventoryPanel : UiPanel
    {
        // SERIALIZED
        [Title("Depend")]
        [SerializeField] [Required] InventoryPanelRecord recordPrefab;
        [SerializeField] [Required] RectTransform recordsParent;
        [SerializeField] [Required] InventoryDraggableItemSlot inventoryDraggableItemSlot;

        // PRIVATE
        readonly Dictionary<string, InventoryPanelRecord> itemRecords = new();

        EventListener refreshInventoryRequestListener;

        // UNITY EVENTS
        protected override void Awake()
        {
            base.Awake();

            refreshInventoryRequestListener = new EventListener(OnRefreshRequested);
            EventManager.RegisterListener<RefreshInventoryRequestEvent>(refreshInventoryRequestListener);
        }
        
        void OnEnable()
        {
            InstantiateRecords();
        }

        void OnDisable()
        {
            DestroyRecords();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            EventManager.UnregisterListener<RefreshInventoryRequestEvent>(refreshInventoryRequestListener);
            refreshInventoryRequestListener = null;
        }
        


        // METHODS
        InventoryPanelRecord InstantiateInventoryPanelRecord(string itemGuid, int itemCount, Transform parent)
        {
            ItemData itemData = ServicesManager.ItemsService.GetItem(itemGuid);
                
            InventoryPanelRecord record = Instantiate(recordPrefab, parent);
            record.Init(itemGuid, itemData.Name, itemCount);
            record.DraggableItem.PreviousSlot = inventoryDraggableItemSlot;
            itemRecords.Add(itemGuid, record);

            return record;
        }

        protected override void TryShowPanel(Type state)
        {
            CachedGameObject.SetActive(state == typeof(InventoryGameState)
                                       || state == typeof(CraftingGameState));
        }

        void InstantiateRecords()
        {
            if(ServicesManager.PlayerInventoryService.PlayerInventory == null) return;
            
            foreach ((string itemGuid, int itemCount) in ServicesManager.PlayerInventoryService.PlayerInventory)
            {
                InstantiateInventoryPanelRecord(itemGuid, itemCount, recordsParent);
            }
        }

        void DestroyRecords()
        {
            foreach (KeyValuePair<string,InventoryPanelRecord> pair in itemRecords)
            {
                Destroy(pair.Value.CachedGameObject);
            }

            itemRecords.Clear();
        }
        
        void OnRefreshRequested(EventBase eventBase)
        {
            DestroyRecords();
            InstantiateRecords();
        }
    }
}