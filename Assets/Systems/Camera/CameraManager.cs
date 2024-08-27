using Cinemachine;
using Sirenix.OdinInspector;
using Systems.Core.GameEvents;
using UnityEngine;

namespace Systems.Camera
{
    public class CameraManager : MonoBehaviour
    {
        [Title("Depend")]
        [SerializeField] [Required] UnityEngine.Camera mainCamera;
        [SerializeField] [Required] CinemachineVirtualCamera playerFollowCamera;
        
        EventListener onPlayerSpawnedListener;

        void Awake()
        {
            onPlayerSpawnedListener = new EventListener(OnPlayerSpawned);
            EventManager.RegisterListener<PlayerSpawnedEvent>(onPlayerSpawnedListener);
        }

        void OnDestroy()
        {
            EventManager.UnregisterListener<PlayerSpawnedEvent>(onPlayerSpawnedListener);
            onPlayerSpawnedListener = null;
        }

        void OnPlayerSpawned(EventBase eventBase)
        {
            PlayerSpawnedEvent playerSpawnedEvent = eventBase as PlayerSpawnedEvent;

            playerFollowCamera.m_Follow = playerSpawnedEvent.PlayerCameraTarget;
        }
    }
}
