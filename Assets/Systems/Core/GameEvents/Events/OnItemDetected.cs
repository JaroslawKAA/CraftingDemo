namespace Systems.Core.GameEvents.Events
{
    public class OnItemDetected : EventBase
    {
        public string Guid { get; private set; }

        public OnItemDetected(string guid)
        {
            Guid = guid;
        }
    }
}
