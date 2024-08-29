namespace Systems.Core.Services
{
    public interface IItemsService
    {
        void RegisterItem(ItemData itemData);
        void UnregisterItem(ItemData itemData);
        ItemData GetItem(string guid);
    }
    
    public struct ItemData
    {
        public readonly string Guid;
        public readonly string Name;

        public ItemData(string guid, string name)
        {
            Guid = guid;
            Name = name;
        }
    }
}
