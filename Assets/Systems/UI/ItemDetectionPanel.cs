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
        [SerializeField] [Required] RectTransform itemNameRectTransform;
        [SerializeField] [Required] RectTransform canvasRectTransform;

        // PRIVATE
        EventListener onItemDetectedListener;
        EventListener onItemReleasedListener;

        Camera mainCamera;

        MeshRenderer selectedItemMeshRenderer;
        Transform selectedItemTransform;

        // UNITY EVENTS
        void Awake()
        {
            onItemDetectedListener = new EventListener(OnItemDetected);
            EventManager.RegisterListener<ItemDetectedEvent>(onItemDetectedListener);

            onItemReleasedListener = new EventListener(OnItemReleased);
            EventManager.RegisterListener<ItemReleasedEvent>(onItemReleasedListener);

            mainCamera = Camera.main;

            itemNameText.enabled = false;
        }

        void Update()
        {
            if (!selectedItemMeshRenderer) return;

            SetItemNameTextAboveItem();
        }

        void SetItemNameTextAboveItem()
        {
            Bounds selectedItemBounds = selectedItemMeshRenderer.bounds;
            Vector3 textTargetPosition = selectedItemBounds.center + new Vector3(0, selectedItemBounds.extents.y);
            Vector2 screenPosition = mainCamera.WorldToScreenPoint(textTargetPosition);
            
            Vector2 screenPositionRation = new Vector2(screenPosition.x / Screen.width,
                screenPosition.y / Screen.height);
            Vector2 itemNameTextCanvasPosition = new Vector2(screenPositionRation.x * canvasRectTransform.sizeDelta.x,
                screenPositionRation.y * canvasRectTransform.sizeDelta.y);
            
            itemNameRectTransform.anchoredPosition = itemNameTextCanvasPosition;
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

            selectedItemMeshRenderer = itemDetectedEvent.ItemMeshRenderer;
            selectedItemTransform = itemDetectedEvent.ItemTransform;

            itemNameText.text = itemDetectedEvent.ItemName;
            itemNameText.enabled = true;
        }

        void OnItemReleased(EventBase eventBase)
        {
            itemNameText.enabled = false;
            itemNameText.text = String.Empty;

            selectedItemMeshRenderer = null;
        }
    }
}