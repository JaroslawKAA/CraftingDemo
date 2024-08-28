using System;
using Systems.Core.GameEvents;
using UnityEngine.Assertions;
using Sirenix.OdinInspector;
using Systems.Core.GameState;
using UnityEngine;

namespace Systems.Core
{
    public class GameManager : MonoBehaviour
    {
        [Title("Depend")]
        [SerializeField] [Required] GameObject playerPrefab;
        [SerializeField] [Required] Transform playerSpawnPoint;
        
        GameObject player;

        GameStateMachine gameStateMachine;

        void Awake()
        {
            gameStateMachine = new GameStateMachine(context: this);
        }

        void Start()
        {
            SpawnPlayer();

            Cursor.visible = false;
            
            gameStateMachine.TransitionTo(GameStateMachine.State.ThirdPersonPlayer);
        }

        void Update()
        {
            gameStateMachine.Update();
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
