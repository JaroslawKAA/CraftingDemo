using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Systems.Core;
using Systems.Core.GameState;
using Systems.Core.Services;
using UnityEngine;

namespace Systems.UI
{
    public class InventoryPanel : UiPanel
    {
        // SERIALIZED
        [Title("Depend")]
        [SerializeField] [Required] InventoryPanelRecord recordPrefab;
        [SerializeField] [Required] RectTransform recordsParent;

        // PRIVATE
        readonly Dictionary<string, InventoryPanelRecord> itemRecords = new();

        // UNITY EVENTS
        protected override void Start()
        {
            base.Start();
            GameManager.Instance.onGameStateChanged += TryShowInventory;
        }

        void OnEnable()
        {
            InstantiateRecords();
        }

        void OnDisable()
        {
            DestroyRecords();
        }

        void OnDestroy()
        {
            GameManager.Instance.onGameStateChanged -= TryShowInventory;
        }

        // METHODS
        void TryShowInventory(Type state)
        {
            CachedGameObject.SetActive(state == typeof(InventoryGameState)
                                       || state == typeof(CraftingGameState));
        }

        void InstantiateRecords()
        {
            if(ServicesManager.PlayerInventoryService.PlayerInventory == null) return;
            
            foreach ((string itemGuid, int itemCount) in ServicesManager.PlayerInventoryService.PlayerInventory)
            {
                ItemData itemData = ServicesManager.ItemsService.GetItem(itemGuid);
                InventoryPanelRecord record = Instantiate(recordPrefab, recordsParent);
                record.Init(itemGuid, itemData.Name, itemCount);
                itemRecords.Add(itemGuid, record);
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
    }
}