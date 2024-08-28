
using UnityEngine;

namespace Systems.InteractionSystem
{
    public interface IIteractable
    {
        string InstanceGuid { get; }
        Transform CachedTransform { get; }
        MeshRenderer MeshRenderer { get; }
        string ItemName { get; }
        void Interact();
    }
}
