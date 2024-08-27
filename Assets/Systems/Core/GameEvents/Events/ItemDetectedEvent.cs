using UnityEngine;

namespace Systems.Core.GameEvents.Events
{
    public class ItemDetectedEvent : EventBase
    {
        public string ItemGuid { get; private set; }
        public string ItemName { get; private set; }
        public Transform ItemTransform { get; private set; }

        public ItemDetectedEvent(string itemGuid, string itemName, Transform itemTransform)
        {
            ItemGuid = itemGuid;
            ItemName = itemName;
            ItemTransform = itemTransform;
        }
    }
}
