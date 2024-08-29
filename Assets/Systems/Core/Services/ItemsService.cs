using System.Collections.Generic;

namespace Systems.Core.Services
{
    public class ItemsService : IItemsService
    {
        Dictionary<string, ItemData> itemDataColection = new();
        
        public void RegisterItem(ItemData itemData) => itemDataColection.Add(itemData.Guid, itemData);
        public void UnregisterItem(ItemData itemData) => itemDataColection.Remove(itemData.Guid);
        public ItemData GetItem(string guid) => itemDataColection[guid];
    }
}
