using System;
using Sirenix.OdinInspector;
using Systems.Core.GameEvents;
using Systems.Core.GameEvents.Events;
using TMPro;
using UnityEngine;

namespace Systems.UI
{
    public class ItemDetectionPanel : MonoBehaviour
    {
        // SERIALIZED
        [Title("Depend")]
        [SerializeField] [Required] TMP_Text itemNameText;

        // PRIVATE
        EventListener onItemDetectedListener;
        EventListener onItemReleasedListener;

        Transform selectedItemTransform;
        string selectedItemGuid;

        // UNITY EVENTS
        void Awake()
        {
            onItemDetectedListener = new EventListener(OnItemDetected);
            EventManager.RegisterListener<ItemDetectedEvent>(onItemDetectedListener);

            onItemReleasedListener = new EventListener(OnItemReleased);
            EventManager.RegisterListener<ItemReleasedEvent>(onItemReleasedListener);

            itemNameText.enabled = false;
        }

        void Update()
        {
            if (!selectedItemTransform) return;

            SetItemNameTextAboveItem();
        }

        void SetItemNameTextAboveItem()
        {
            throw new NotImplementedException();
        }

        void OnDestroy()
        {
            EventManager.UnregisterListener<ItemDetectedEvent>(onItemReleasedListener);
            onItemDetectedListener = null;
        }

        // METHODS
        void OnItemDetected(EventBase eventBase)
        {
            ItemDetectedEvent itemDetectedEvent = eventBase as ItemDetectedEvent;

            selectedItemTransform = itemDetectedEvent.ItemTransform;
            selectedItemGuid = itemDetectedEvent.ItemGuid;

            itemNameText.text = itemDetectedEvent.ItemName;
            itemNameText.enabled = true;
        }

        void OnItemReleased(EventBase eventBase)
        {
            itemNameText.enabled = false;
            itemNameText.text = String.Empty;

            selectedItemTransform = null;
            selectedItemGuid = null;
        }
    }
}