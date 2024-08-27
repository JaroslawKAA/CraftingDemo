using Systems.Core.GameEvents;
using UnityEngine;

public class PlayerSpawnedEvent : EventBase
{
    public Transform PlayerCameraTarget { get; private set; }
    public GameObject Player { get; private set; }

    public PlayerSpawnedEvent(Transform playerCameraTarget, GameObject player)
    {
        PlayerCameraTarget = playerCameraTarget;
        Player = player;
    }
}
