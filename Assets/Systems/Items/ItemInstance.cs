using System;
using Sirenix.OdinInspector;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Systems.Items
{
    public class ItemInstance : MonoBehaviour
    {
        // SERIALIZED
        [Title("Depend")]
        [SerializeField] [Required] [InlineEditor] Item item;

        [Title("Debug")]
        [SerializeField] [ReadOnly] string instanceGuid;
        
        // UNITY EVENTS
        void Awake()
        {
            instanceGuid = Guid.NewGuid().ToString();
        }

        // METHODS
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
            
            EditorUtility.SetDirty(gameObject);
        }
#endif
    }
}
