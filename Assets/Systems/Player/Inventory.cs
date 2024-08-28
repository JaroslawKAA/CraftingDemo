using System.Collections.Generic;
using UnityEngine;

namespace Systems.Player
{
    public class Inventory : MonoBehaviour
    {
        // PRIVATE
        Dictionary<string, int> itemsCount = new();

        // PROPERTIES
        public IReadOnlyDictionary<string, int> ItemsCount => itemsCount;

        // METHODS
        public void AddItem(string guid, int count)
        {
            if (!itemsCount.TryAdd(guid, count))
            {
                itemsCount[guid] += count;
            }
        }

        public void RemoveItem(string guid, int count)
        {
            if (itemsCount.ContainsKey(guid))
            {
                if (itemsCount[guid] > count)
                {
                    itemsCount[guid] -= count;
                }
                else if (itemsCount[guid] == count)
                {
                    itemsCount.Remove(guid);
                }
                else
                {
                    Debug.LogError($"Is not possible to remove item |{guid} - {count}|, " +
                                   $"because in inventory exist only {itemsCount[guid]} items");
                }
            }
            else
            {
                Debug.LogError($"No item |{guid} - {count}| in inventory to remove");
            }
        }
    }
}
