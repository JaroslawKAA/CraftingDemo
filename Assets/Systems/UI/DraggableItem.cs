using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Systems.UI
{
    public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [Title("Depend")]
        [SerializeField] [Required] Image image;
        [SerializeField] [Required] TMP_Text tmpText;
        
        [Title("Debug")]
        [ShowInInspector] [ReadOnly] public Transform ParentAfterDrag { get; set; }
        [ShowInInspector] [ReadOnly] public DraggableSlotBase PreviousSlot { get; set; }

        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("Begin drag item");
            ParentAfterDrag = transform.parent;
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();

            DisableRaycastTarget();
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("End drag item");
            transform.SetParent(ParentAfterDrag);
            EnableRaycastTarget();
        }

        public void EnableRaycastTarget()
        {
            image.raycastTarget = true;
            tmpText.raycastTarget = true;
        }

        void DisableRaycastTarget()
        {
            image.raycastTarget = false;
            tmpText.raycastTarget = false;
        }
    }
}
