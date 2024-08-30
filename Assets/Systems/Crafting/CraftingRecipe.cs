using Sirenix.OdinInspector;
using Systems.Items;
using UnityEngine;
using UnityEngine.Events;

namespace Systems.Crafting
{
    
    [CreateAssetMenu(menuName = "Crafting/CraftingRecipe", fileName = "CraftingRecipe", order = 0)]
    public class CraftingRecipe : ScriptableObject
    {
        [SerializeReference] [Required] public Item firstItem;
        [SerializeReference] [Required] public Item secondItem;
        [SerializeReference] [Required] public Item result;
        [SerializeField] [Range(1, 100)] public int successChance = 100;
        [SerializeField] public UnityEvent onCraftingSuccess;
        [SerializeField] public UnityEvent onCraftingFailed;
    }
}
