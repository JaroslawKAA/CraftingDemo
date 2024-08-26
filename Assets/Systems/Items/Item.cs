using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Systems.Items
{
    [CreateAssetMenu(menuName = "Items/Item", fileName = "Item", order = 0)]
    public class Item : ScriptableObject
    {
        [Title("Config")]
        [SerializeField] [ReadOnly] [Required] string guid;
        [SerializeField] [Required] string itemName;
        [SerializeField] [Required] Sprite icon;
        [SerializeField] [Required] GameObject prefab;
        
        public string Guid => guid;
        public Sprite Icon => icon;
        public GameObject Prefab => prefab;
        public string ItemName => itemName;

#if UNITY_EDITOR
        [Button]
        void GenerateGuid()
        {
            guid = System.Guid.NewGuid().ToString();
            EditorUtility.SetDirty(this);
        }
#endif
    }
}
