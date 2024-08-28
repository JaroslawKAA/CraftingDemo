namespace Systems.Core.GameEvents.Events
{
    public class PickUpItemEvent : EventBase
    {
        public string ItemGuid { get; private set; }

        public PickUpItemEvent(string itemGuid)
        {
            ItemGuid = itemGuid;
        }
    }
}
