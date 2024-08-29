using UnityEngine;
using UnityEngine.EventSystems;

namespace Systems.UI
{
    public class DraggableItemSlot : MonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            if(transform.childCount == 0)
            {
                GameObject dropped = eventData.pointerDrag;
                DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
                draggableItem.ParentAfterDrag = transform;
            }
        }
    }
}
