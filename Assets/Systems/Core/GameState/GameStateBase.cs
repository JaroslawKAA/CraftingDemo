using Systems.Core.Patterns.State;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Systems.Core.GameState
{
    public abstract class GameStateBase : StateBase
    {
        protected GameStateMachine gameStateMachine;
        
        protected GameStateBase(StateMachineBase stateMachineBase, MonoBehaviour context) : base(stateMachineBase, context)
        {
            gameStateMachine = stateMachineBase as GameStateMachine;
        }
        
        protected void ToInventoryState(InputAction.CallbackContext _)
        {
            gameStateMachine.TransitionTo(GameStateMachine.State.Inventory);
        }
        
        protected void ToCraftingState(InputAction.CallbackContext _)
        {
            gameStateMachine.TransitionTo(GameStateMachine.State.Crafting);
        }
        
        protected void ToThirdPersonPlayerState(InputAction.CallbackContext _)
        {
            gameStateMachine.TransitionTo(GameStateMachine.State.ThirdPersonPlayer);
        }
    }
}
