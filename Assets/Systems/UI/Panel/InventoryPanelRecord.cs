using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Systems.UI.Panel
{
    public class InventoryPanelRecord : MonoBehaviour
    {
        [Title("Depend")]
        [SerializeField] [Required] TMP_Text recordText;
        [SerializeField] [Required] DraggableItem draggableItem;
        
        [Title("Debug")]
        [field: SerializeField] [field: ReadOnly] public string ItemGuid { get; private set; }
        [field: SerializeField] [field: ReadOnly] public string ItemName { get; private set; }
        [field: SerializeField] [field: ReadOnly] public int ItemCount { get; private set; }
        public DraggableItem DraggableItem => draggableItem;
        public GameObject CachedGameObject { get; private set; }

        public void Init(string itemGuid, string itemName, int itemCount)
        {
            ItemGuid = itemGuid;
            ItemName = itemName;
            ItemCount = itemCount;

            recordText.text = $"{itemName} ({itemCount})";
            CachedGameObject = gameObject;
        }
    }
}
