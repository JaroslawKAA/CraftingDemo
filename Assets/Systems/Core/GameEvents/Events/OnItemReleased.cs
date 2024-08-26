namespace Systems.Core.GameEvents.Events
{
    public class OnItemReleased : EventBase
    {
        public string Guid { get; private set; }

        public OnItemReleased(string guid)
        {
            Guid = guid;
        }
    }
}