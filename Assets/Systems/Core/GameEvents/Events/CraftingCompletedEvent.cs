namespace Systems.Core.GameEvents.Events
{
    public class CraftingCompletedEvent : EventBase
    {
        public string ResultItemGuid { get; private set; }

        public CraftingCompletedEvent(string resultItemGuid)
        {
            ResultItemGuid = resultItemGuid;
        }
    }
}
