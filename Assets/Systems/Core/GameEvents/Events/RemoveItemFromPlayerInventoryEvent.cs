namespace Systems.Core.GameEvents.Events
{
    public class RemoveItemFromPlayerInventoryEvent : EventBase
    {
        public string ItemGuid { get; private set; }

        public RemoveItemFromPlayerInventoryEvent(string itemGuid)
        {
            ItemGuid = itemGuid;
        }
    }
}
