using System;
using Systems.Core.GameEvents;
using UnityEngine.Assertions;
using Sirenix.OdinInspector;
using Systems.Core.GameState;
using Systems.Core.Patterns.Singleton;
using Systems.Core.Patterns.State;
using UnityEngine;

namespace Systems.Core
{
    public class GameManager : SimpleSingleton<GameManager>
    {
        // SERIALIZED
        [Title("Depend")]
        [SerializeField] [Required] GameObject playerPrefab;
        [SerializeField] [Required] Transform playerSpawnPoint;
        
        // PRIVATE
        GameObject player;

        GameStateMachine gameStateMachine;
        
        // EVENTS
        public event Action<Type> onGameStateChanged; 

        // UNITY EVENTS
        void Awake()
        {
            gameStateMachine = new GameStateMachine(context: this);
            gameStateMachine.onStateChanged += OnGameStateChanged;
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

        void OnDestroy()
        {
            gameStateMachine.onStateChanged -= OnGameStateChanged;
        }

        // METHODS
        void OnGameStateChanged(StateBase state) => onGameStateChanged?.Invoke(typeof(StateBase));

        void SpawnPlayer()
        {
            player = Instantiate(playerPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation);
            
            Transform playerCameraRoot = player.transform.Find("PlayerCameraRoot");
            Assert.IsNotNull(playerCameraRoot);
            
            EventManager.TriggerEvent(new PlayerSpawnedEvent(playerCameraRoot, player));
        }
    }
}
