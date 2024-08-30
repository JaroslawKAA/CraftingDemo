using Systems.Core.GameEvents;
using Systems.Core.GameEvents.Events;
using Systems.UI.Panel;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Systems.UI
{
    public class ThrowDraggableFromInventorySlot : DraggableSlotBase, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            GameObject dropped = eventData.pointerDrag;
            DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
            
            if (draggableItem.PreviousSlot is CraftingDraggableItemSlot)
            {
                Destroy(dropped);
            }
            else if (draggableItem.PreviousSlot is InventoryDraggableItemSlot)
            {
                InventoryPanelRecord inventoryPanelRecord = dropped.GetComponent<InventoryPanelRecord>();
                
                EventManager.TriggerEvent(new ThrowItemFromInventoryEvent(inventoryPanelRecord.ItemGuid));
            }
        }
    }
}
