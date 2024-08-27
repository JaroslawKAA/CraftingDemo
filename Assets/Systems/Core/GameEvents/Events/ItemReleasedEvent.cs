namespace Systems.Core.GameEvents.Events
{
    public class ItemReleasedEvent : EventBase
    {
        public string Guid { get; private set; }

        public ItemReleasedEvent(string guid)
        {
            Guid = guid;
        }
    }
}