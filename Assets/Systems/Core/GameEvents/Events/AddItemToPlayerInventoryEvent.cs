namespace Systems.Core.GameEvents.Events
{
    public class AddItemToPlayerInventoryEvent : EventBase
    {
        public string ItemGuid { get; private set; }

        public AddItemToPlayerInventoryEvent(string itemGuid)
        {
            ItemGuid = itemGuid;
        }
    }
}
