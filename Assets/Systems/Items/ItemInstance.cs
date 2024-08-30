using System;
using Sirenix.OdinInspector;
using Systems.Core.GameEvents;
using Systems.Core.GameEvents.Events;
using Systems.InteractionSystem;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Systems.Items
{
    public class ItemInstance : MonoBehaviour, IIteractable
    {
        // SERIALIZED
        [Title("Depend")]
        [SerializeField] [Required] [InlineEditor] Item item;
        [SerializeField] [Required] MeshRenderer meshRenderer;

        [Title("Debug")]
        [SerializeField] [ReadOnly] string instanceGuid;

        // PROPERTIES
        public string InstanceGuid => instanceGuid;
        public Item Item => item;
        public MeshRenderer MeshRenderer => meshRenderer;
        public Transform CachedTransform { get; private set; }
        public string ItemName => Item.ItemName;

        // UNITY EVENTS
        void Awake()
        {
            instanceGuid = Guid.NewGuid().ToString();
            ItemsManager.RegisterInstance(this);

            CachedTransform = transform;
        }

        // METHODS
        public void Interact()
        {
            EventManager.TriggerEvent(new AddItemToPlayerInventoryEvent(Item.Guid));
            
            // TODO Return item to pool
            Destroy(gameObject);
        }

#if UNITY_EDITOR
        [Button]
        void ConfigureItem()
        {
            if (!TryGetComponent(out Collider _))
            {
                gameObject.AddComponent<BoxCollider>();
            }

            if (!TryGetComponent(out Rigidbody _))
            {
                Rigidbody rb = gameObject.AddComponent<Rigidbody>();
                rb.constraints = RigidbodyConstraints.FreezeRotationY
                                 | RigidbodyConstraints.FreezeRotationX
                                 | RigidbodyConstraints.FreezePositionZ
                                 | RigidbodyConstraints.FreezePositionX
                                 | RigidbodyConstraints.FreezeRotationZ;
            }

            gameObject.layer = Layers.Item;

            meshRenderer = GetComponent<MeshRenderer>();

            EditorUtility.SetDirty(gameObject);
        }
#endif
    }
}