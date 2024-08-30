using UnityEngine;
using UnityEngine.EventSystems;

namespace Systems.UI
{
    public class CraftingDraggableItemSlot : DraggableSlotBase, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            GameObject dropped = eventData.pointerDrag;
            DraggableItem droppedDraggableItem = dropped.GetComponent<DraggableItem>();
            DraggableItem[] draggableItemsInSlot = GetComponentsInChildren<DraggableItem>();
            
            int draggableItemsInSlotCount = draggableItemsInSlot.Length;

            bool isSlotFree = draggableItemsInSlotCount == 0;
            bool isSlotOccupiedByOneItem = draggableItemsInSlotCount == 1;

            if(isSlotFree)
            {
                if (droppedDraggableItem.PreviousSlot  is CraftingDraggableItemSlot)
                {
                    DropDraggableItem(droppedDraggableItem);
                }
                else if (droppedDraggableItem.PreviousSlot is InventoryDraggableItemSlot)
                {
                    InstantiateDraggableCopy(dropped);
                }
            }
            else if (isSlotOccupiedByOneItem)
            {
                DraggableItem currentlyInSlot = draggableItemsInSlot[0];

                if (droppedDraggableItem.PreviousSlot is CraftingDraggableItemSlot)
                {
                    SwapItems(dropped, currentlyInSlot);

                }
                else if (droppedDraggableItem.PreviousSlot is InventoryDraggableItemSlot)
                {
                    Destroy(currentlyInSlot.gameObject);
                    InstantiateDraggableCopy(dropped);
                }
            }
        }

        void DropDraggableItem(DraggableItem droppedDraggableItem)
        {
            droppedDraggableItem.ParentAfterDrag = transform;
            droppedDraggableItem.PreviousSlot = this;
        }

        void SwapItems(GameObject dropped, DraggableItem currentlyInSlot)
        {
            DraggableItem droppedDraggable = dropped.GetComponent<DraggableItem>();
            
            currentlyInSlot.transform.SetParent(droppedDraggable.ParentAfterDrag);
            currentlyInSlot.PreviousSlot = droppedDraggable.PreviousSlot;
            
            droppedDraggable.ParentAfterDrag = transform;
            droppedDraggable.PreviousSlot = this;
        }

        void InstantiateDraggableCopy(GameObject dropped)
        {
            GameObject droppedCopy = Instantiate(dropped, transform);
                
            DraggableItem draggableItem = droppedCopy.GetComponent<DraggableItem>();
            draggableItem.PreviousSlot = this;
            draggableItem.EnableRaycastTarget();
        }
    }
}
