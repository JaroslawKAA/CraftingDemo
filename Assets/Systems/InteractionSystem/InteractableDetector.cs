using Input = Systems.Core.InputSystem.InputsManager;

using Sirenix.OdinInspector;
using Systems.Core.GameEvents;
using Systems.Core.GameEvents.Events;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Systems.InteractionSystem
{
    public class InteractableDetector : MonoBehaviour
    {
        // SERIALIZED
        [Title("Config")]
        [SerializeField] float detectionDistance = 5f;

        // PRIVATE
        EventListener onPlayerSpawnedListener;

        Transform detectionPoint;

        Collider[] itemDetectionResult = new Collider[20];

        InputAction onInteract;

        // PROPERTIES
        IIteractable DetectedItem { get; set; }
        IIteractable PreviouslyDetectedItem { get; set; }

        // UNITY EVENTS
        void Awake()
        {
            onPlayerSpawnedListener = new EventListener(OnPlayerSpawned);
            EventManager.RegisterListener<PlayerSpawnedEvent>(onPlayerSpawnedListener);
        }

        void OnEnable()
        {
            Input.Inputs.ThirdPersonPlayer.Interact.performed += OnInteract;
        }

        void Update()
        {
            if (!detectionPoint) return;

            TryDetectItem();
        }

        void OnDisable()
        {
            Input.Inputs.ThirdPersonPlayer.Interact.performed -= OnInteract;
        }

        void OnInteract(InputAction.CallbackContext context)
        {
            if (DetectedItem != null)
            {
                DetectedItem.Interact();
            }
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

            DetectedItem = GetClosestItem(detectedItemsCount);

            if (DetectedItem == null)
            {
                if (PreviouslyDetectedItem != null)
                {
                    EventManager.TriggerEvent(new ItemReleasedEvent(PreviouslyDetectedItem.InstanceGuid));
                }
            }
            else
            {
                if (PreviouslyDetectedItem != DetectedItem)
                {
                    EventManager.TriggerEvent(
                        new ItemDetectedEvent(
                            DetectedItem.InstanceGuid,
                            DetectedItem.ItemName,
                            DetectedItem.MeshRenderer,
                            DetectedItem.CachedTransform));
                    
                    PreviouslyDetectedItem = DetectedItem;
                }
            }
        }

        IIteractable GetClosestItem(int detectedItemsCount)
        {
            IIteractable detectedItem = null;
            float closestItemDistance = 10f;

            for (int i = 0; i < detectedItemsCount; i++)
            {
                if (itemDetectionResult[i].TryGetComponent(out IIteractable itemInstance))
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

        bool IsItemInFront(IIteractable itemInstance)
        {
            Vector3 toItemVector = itemInstance.CachedTransform.position - detectionPoint.position;
            float dot = Vector3.Dot(detectionPoint.forward, toItemVector);

            return dot > 0;
        }

        float GetDistanceToItem(IIteractable itemInstance)
        {
            return Vector3.Distance(detectionPoint.position, itemInstance.CachedTransform.position);
        }
    }
}