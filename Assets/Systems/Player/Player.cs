using Systems.Core.GameEvents;
using Systems.Core.GameEvents.Events;
using Systems.Core.Services;
using Systems.Player;
using UnityEngine;

public class Player : MonoBehaviour
{
    // PRIVATE
    Inventory inventory = new();

    EventListener addItemToPlayerInventoryListener;
    EventListener removeItemToPlayerInventoryListener;
    
    // UNITY EVENTS
    void Awake()
    {
        addItemToPlayerInventoryListener = new EventListener(OnAddItemRequested);
        EventManager.RegisterListener<AddItemToPlayerInventoryEvent>(addItemToPlayerInventoryListener);

        removeItemToPlayerInventoryListener = new EventListener(OnRemoveItemRequested);
        EventManager.RegisterListener<RemoveItemFromPlayerInventoryEvent>(removeItemToPlayerInventoryListener);
        
        ServicesManager.PlayerInventoryService.SetPlayerInventory(inventory.ItemsCount);
    }

    void OnDestroy()
    {
        EventManager.UnregisterListener<AddItemToPlayerInventoryEvent>(addItemToPlayerInventoryListener);
        addItemToPlayerInventoryListener = null;

        EventManager.UnregisterListener<RemoveItemFromPlayerInventoryEvent>(removeItemToPlayerInventoryListener);
        removeItemToPlayerInventoryListener = null;
    }

    // METHODS
    void OnAddItemRequested(EventBase eventBase)
    {
        AddItemToPlayerInventoryEvent addItemToPlayerInventoryEvent = eventBase as AddItemToPlayerInventoryEvent;

        inventory.AddItem(addItemToPlayerInventoryEvent.ItemGuid, 1);
        Debug.Log($"The player added an item to inventory |{addItemToPlayerInventoryEvent.ItemGuid}|");
    }
    
    void OnRemoveItemRequested(EventBase eventBase)
    {
        RemoveItemFromPlayerInventoryEvent removeItemFromPlayerInventoryEvent = eventBase as RemoveItemFromPlayerInventoryEvent;
        
        inventory.RemoveItem(removeItemFromPlayerInventoryEvent.ItemGuid, 1);
        Debug.Log($"The player removed an item from inventory |{removeItemFromPlayerInventoryEvent.ItemGuid}|");
    }
}
