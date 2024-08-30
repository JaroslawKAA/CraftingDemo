using UnityEngine;
using UnityEngine.EventSystems;

namespace Systems.UI
{
    public class InventoryDraggableItemSlot : DraggableSlotBase, IDropHandler
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
                draggableItem.ParentAfterDrag = transform;
                draggableItem.PreviousSlot = this;
            }
        }
    }
}
