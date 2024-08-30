using Sirenix.OdinInspector;
using Systems.Core.GameEvents;
using Systems.Core.GameEvents.Events;
using Systems.Core.Services;
using Systems.Player;
using UnityEngine;

public class Player : MonoBehaviour
{
    // SERIALIZED
    [Title("Depend")]
    [SerializeField] [Required] Transform itemDropPosition;

    // PRIVATE
    Inventory inventory = new();

    EventListener addItemToPlayerInventoryListener;
    EventListener removeItemToPlayerInventoryListener;
    EventListener throwItemFromInventoryListener;

    // UNITY EVENTS
    void Awake()
    {
        SubscribeEvents();

        ServicesManager.PlayerInventoryService.SetPlayerInventory(inventory.ItemsCount);
    }

    void OnDestroy()
    {
        UnsubscribeEvents();
    }

    // METHODS
    void SubscribeEvents()
    {
        addItemToPlayerInventoryListener = new EventListener(OnAddItemRequested);
        EventManager.RegisterListener<AddItemToPlayerInventoryEvent>(addItemToPlayerInventoryListener);

        removeItemToPlayerInventoryListener = new EventListener(OnRemoveItemRequested);
        EventManager.RegisterListener<RemoveItemFromPlayerInventoryEvent>(removeItemToPlayerInventoryListener);

        throwItemFromInventoryListener = new EventListener(OnThrowItemFromInventory);
        EventManager.RegisterListener<ThrowItemFromInventoryEvent>(throwItemFromInventoryListener);
    }

    void UnsubscribeEvents()
    {
        EventManager.UnregisterListener<AddItemToPlayerInventoryEvent>(addItemToPlayerInventoryListener);
        addItemToPlayerInventoryListener = null;

        EventManager.UnregisterListener<RemoveItemFromPlayerInventoryEvent>(removeItemToPlayerInventoryListener);
        removeItemToPlayerInventoryListener = null;

        EventManager.UnregisterListener<ThrowItemFromInventoryEvent>(throwItemFromInventoryListener);
        throwItemFromInventoryListener = null;
    }

    void OnAddItemRequested(EventBase eventBase)
    {
        AddItemToPlayerInventoryEvent addItemToPlayerInventoryEvent = eventBase as AddItemToPlayerInventoryEvent;

        inventory.AddItem(addItemToPlayerInventoryEvent.ItemGuid, 1);
        Debug.Log($"The player added an item to inventory |{addItemToPlayerInventoryEvent.ItemGuid}|");
    }

    void OnRemoveItemRequested(EventBase eventBase)
    {
        RemoveItemFromPlayerInventoryEvent removeItemFromPlayerInventoryEvent =
            eventBase as RemoveItemFromPlayerInventoryEvent;

        inventory.RemoveItem(removeItemFromPlayerInventoryEvent.ItemGuid, 1);
        Debug.Log($"The player removed an item from inventory |{removeItemFromPlayerInventoryEvent.ItemGuid}|");
    }

    void OnThrowItemFromInventory(EventBase eventBase)
    {
        ThrowItemFromInventoryEvent throwItemFromInventoryEvent = eventBase as ThrowItemFromInventoryEvent;
        inventory.RemoveItem(throwItemFromInventoryEvent.ItemGuid, 1);

        GameObject itemInstance = ServicesManager.ItemInstancesService.GetItemInstance(throwItemFromInventoryEvent.ItemGuid);
        itemInstance.transform.position = itemDropPosition.position;
        itemInstance.transform.rotation = itemDropPosition.rotation;
        
        Rigidbody itemInstanceRigidBody = itemInstance.GetComponent<Rigidbody>();
        itemInstanceRigidBody.velocity = Vector3.zero;
        itemInstanceRigidBody.angularVelocity = Vector3.zero;
        
        EventManager.TriggerEvent(new RefreshInventoryRequestEvent());
    }
}