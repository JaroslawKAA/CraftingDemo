namespace Systems.Core.GameEvents.Events
{
    public class CraftRequestEvent : EventBase
    {
        public string FirstItemGuid { get; private set; }
        public string SecondItemGuid { get; private set; }

        public CraftRequestEvent(string firstItemGuid, string secondItemGuid)
        {
            FirstItemGuid = firstItemGuid;
            SecondItemGuid = secondItemGuid;
        }
    }
}
