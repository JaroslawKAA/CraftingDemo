using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Systems.UI
{
    public class InventoryPanelRecord : MonoBehaviour
    {
        [Title("Depend")]
        [SerializeField] [Required] TMP_Text recordText;
        
        public string ItemGuid { get; private set; }
        public string ItemName { get; private set; }
        public int ItemCount { get; private set; }
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
