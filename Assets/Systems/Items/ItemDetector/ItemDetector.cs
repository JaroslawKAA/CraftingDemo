using Sirenix.OdinInspector;
using Systems.Core.GameEvents;
using Systems.Core.GameEvents.Events;
using UnityEngine;

namespace Systems.Items.ItemDetector
{
    public class ItemDetector : MonoBehaviour
    {
        // SERIALIZED
        [Title("Config")]
        [SerializeField] float detectionDistance = 5f;

        // PRIVATE
        EventListener onPlayerSpawnedListener;

        Transform detectionPoint;

        Collider[] itemDetectionResult = new Collider[20];

        // PROPERTIES
        ItemInstance PreviouslyDetectedItem { get; set; }

        // UNITY EVENTS
        void Awake()
        {
            onPlayerSpawnedListener = new EventListener(OnPlayerSpawned);
            EventManager.RegisterListener<PlayerSpawnedEvent>(onPlayerSpawnedListener);
        }

        void Update()
        {
            if (!detectionPoint) return;

            TryDetectItem();
        }

        void OnDestroy()
        {
            EventManager.UnregisterListener<PlayerSpawnedEvent>(onPlayerSpawnedListener);
            onPlayerSpawnedListener = null;
        }

        void OnDrawGizmos()
        {
            if (!detectionPoint) return;

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(detectionPoint.position, detectionDistance);
        }

        // METHODS
        void OnPlayerSpawned(EventBase eventBase)
        {
            PlayerSpawnedEvent playerSpawnedEvent = eventBase as PlayerSpawnedEvent;

            detectionPoint = playerSpawnedEvent.Player.transform;
        }

        void TryDetectItem()
        {
            int detectedItemsCount = Physics.OverlapSphereNonAlloc(position: detectionPoint.position,
                radius: detectionDistance,
                results: itemDetectionResult,
                layerMask: ~Layers.Item);

            ItemInstance detectedItem = GetClosestItem(detectedItemsCount);

            if (detectedItem == null)
            {
                if (PreviouslyDetectedItem != null)
                {
                    EventManager.TriggerEvent(new ItemReleasedEvent(PreviouslyDetectedItem.InstanceGuid));
                }
            }
            else
            {
                if (PreviouslyDetectedItem != detectedItem)
                {
                    EventManager.TriggerEvent(
                        new ItemDetectedEvent(
                            detectedItem.InstanceGuid,
                            detectedItem.Item.ItemName,
                            detectedItem.MeshRenderer,
                            detectedItem.CachedTransform));
                    
                    PreviouslyDetectedItem = detectedItem;
                }
            }
        }

        ItemInstance GetClosestItem(int detectedItemsCount)
        {
            ItemInstance detectedItem = null;
            float closestItemDistance = 10f;

            for (int i = 0; i < detectedItemsCount; i++)
            {
                if (itemDetectionResult[i].TryGetComponent(out ItemInstance itemInstance))
                {
                    float distanceToItem = GetDistanceToItem(itemInstance);
                    if (IsItemInFront(itemInstance) && distanceToItem < closestItemDistance)
                    {
                        detectedItem = itemInstance;
                        closestItemDistance = distanceToItem;
                    }
                }
            }

            return detectedItem;
        }

        bool IsItemInFront(ItemInstance itemInstance)
        {
            Vector3 toItemVector = itemInstance.CachedTransform.position - detectionPoint.position;
            float dot = Vector3.Dot(detectionPoint.forward, toItemVector);

            return dot > 0;
        }

        float GetDistanceToItem(ItemInstance itemInstance)
        {
            return Vector3.Distance(detectionPoint.position, itemInstance.CachedTransform.position);
        }
    }
}