using Systems.Core.GameEvents;
using Systems.Core.GameEvents.Events;
using Systems.Player;
using UnityEngine;

public class Player : MonoBehaviour
{
    Inventory inventory = new();

    EventListener itemPickedUpListener;
    
    void Awake()
    {
        itemPickedUpListener = new EventListener(OnItemPickedUp);
        EventManager.RegisterListener<PickUpItemEvent>(itemPickedUpListener);
    }

    void OnDestroy()
    {
        EventManager.UnregisterListener<PickUpItemEvent>(itemPickedUpListener);
        itemPickedUpListener = null;
    }

    void OnItemPickedUp(EventBase eventBase)
    {
        PickUpItemEvent pickUpItemEvent = eventBase as PickUpItemEvent;

        inventory.AddItem(pickUpItemEvent.ItemGuid, 1);
    }
}
