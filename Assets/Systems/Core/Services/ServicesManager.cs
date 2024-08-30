namespace Systems.Core.Services
{
    public static class ServicesManager
    {
        public static IItemsService ItemsService { get; private set; }
        public static IPlayerInventoryService PlayerInventoryService { get; private set; }
        public static IItemInstancesService ItemInstancesService { get; private set; }

        static ServicesManager()
        {
            ItemsService = new ItemsService();
            ItemInstancesService = new ItemInstancesService();
            PlayerInventoryService = new PlayerInventoryService();
        }
    }
}
