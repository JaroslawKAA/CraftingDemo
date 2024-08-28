using Systems.Core.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Systems.Core.GameState
{
    public class ThirdPersonPlayerGameState : GameStateBase
    {
        Inputs.ThirdPersonPlayerActions ThirdPersonPlayerActions = InputsManager.Inputs.ThirdPersonPlayer;
        
        public ThirdPersonPlayerGameState(StateMachineBase stateMachineBase, MonoBehaviour context) 
            : base(stateMachineBase, context)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            ThirdPersonPlayerActions.Enable();
            ThirdPersonPlayerActions.Inventory.performed += ToInventoryState;
        }

        public override void OnExit()
        {
            base.OnExit();
            ThirdPersonPlayerActions.Disable();
            ThirdPersonPlayerActions.Inventory.performed -= ToInventoryState;
        }

        void ToInventoryState(InputAction.CallbackContext context)
        {
            gameStateMachine.TransitionTo(GameStateMachine.State.Inventory);
        }
    }
}
