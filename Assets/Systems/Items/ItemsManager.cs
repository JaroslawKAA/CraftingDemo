using System.Collections.Generic;
using UnityEngine;

namespace Systems.Items
{
    public static class ItemsManager
    {
        // PRIVATE
        static Dictionary<string, Item> itemTypes = new();
        static Dictionary<string, ItemInstance> itemInstances = new();

        static ItemsManager()
        {
            LoadItemTypes();
        }

        // METHODS
        public static Item GetItem(string guid) => itemTypes[guid];
        public static ItemInstance GetItemInstance(string guid) => itemInstances[guid];

        public static void RegisterInstance(ItemInstance itemInstance)
        {
            itemInstances.Add(itemInstance.InstanceGuid, itemInstance);
        }

        public static void UnregisterInstance(ItemInstance itemInstance)
        {
            itemInstances.Remove(itemInstance.InstanceGuid);
        }

        static void LoadItemTypes()
        {
            Item[] allItems = Resources.LoadAll<Item>("Items");
            foreach (Item item in allItems) 
                itemTypes.Add(item.Guid, item);
        }
    }
}
