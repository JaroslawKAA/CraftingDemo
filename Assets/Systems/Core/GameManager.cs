using System;
using Systems.Core.GameEvents;
using UnityEngine.Assertions;
using Sirenix.OdinInspector;
using Systems.Core.Services;
using UnityEngine;

namespace Systems.Core
{
    public class GameManager : MonoBehaviour
    {
        [Title("Depend")]
        [SerializeField] [Required] GameObject playerPrefab;
        [SerializeField] [Required] Transform playerSpawnPoint;
        
        IItemDetectionService itemDetectionService;

        GameObject player;

        void Awake()
        {
            itemDetectionService = new ItemDetectionService();
        }

        void Start()
        {
            SpawnPlayer();
        }

        void SpawnPlayer()
        {
            player = Instantiate(playerPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation);
            
            Transform playerCameraRoot = player.transform.Find("PlayerCameraRoot");
            Assert.IsNotNull(playerCameraRoot);
            
            EventManager.TriggerEvent(new PlayerSpawnedEvent(playerCameraRoot, player));
        }
    }
}
