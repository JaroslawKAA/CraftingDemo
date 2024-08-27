using UnityEngine;

namespace Systems.Core.GameEvents.Events
{
    public class ItemDetectedEvent : EventBase
    {
        public string ItemGuid { get; private set; }
        public string ItemName { get; private set; }
        public MeshRenderer ItemMeshRenderer { get; private set; }
        public Transform ItemTransform { get; private set; }

        public ItemDetectedEvent(string itemGuid, string itemName, MeshRenderer itemMeshRenderer, Transform itemTransform)
        {
            ItemGuid = itemGuid;
            ItemName = itemName;
            ItemMeshRenderer = itemMeshRenderer;
            ItemTransform = itemTransform;
        }
    }
}
