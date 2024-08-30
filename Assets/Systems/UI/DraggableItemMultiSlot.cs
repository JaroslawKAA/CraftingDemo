using UnityEngine;
using UnityEngine.EventSystems;

namespace Systems.UI
{
    public class DraggableItemMultiSlot : MonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            GameObject dropped = eventData.pointerDrag;
            DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
            draggableItem.ParentAfterDrag = transform;
        }
    }
}
