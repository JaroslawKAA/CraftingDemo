using System;
using UnityEngine;

namespace Systems.Core.GameState
{
    public class GameStateMachine : StateMachineBase
    {
        public enum State
        {
            ThirdPersonPlayer,
            Inventory,
            Crafting,
        }
        
        // PROPERTIES
        ThirdPersonPlayerGameState ThirdPersonPlayerGameState { get; }
        InventoryGameState InventoryGameState { get; }
        CraftingGameState CraftingGameState { get; }

        // METHODS
        public GameStateMachine(MonoBehaviour context)
        {
            ThirdPersonPlayerGameState = new ThirdPersonPlayerGameState(this, context);
            InventoryGameState = new InventoryGameState(this, context);
            CraftingGameState = new CraftingGameState(this, context);
        }

        public void TransitionTo(State state)
        {
            switch (state)
            {
                case State.ThirdPersonPlayer:
                    TransitionTo(ThirdPersonPlayerGameState);
                    break;
                case State.Inventory:
                    TransitionTo(InventoryGameState);
                    break;
                case State.Crafting:
                    TransitionTo(CraftingGameState);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
    }
}
