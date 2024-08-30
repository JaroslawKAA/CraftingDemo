namespace Systems.Core.GameEvents.Events
{
    public class ThrowItemFromInventoryEvent : EventBase
    {
        public string ItemGuid { get; private set; }

        public ThrowItemFromInventoryEvent(string itemGuid)
        {
            ItemGuid = itemGuid;
        }
    }
}
