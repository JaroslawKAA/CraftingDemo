using Systems.Core.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Systems.Core.GameState
{
    public class ThirdPersonPlayerGameState : GameStateBase
    {
        Inputs.ThirdPersonPlayerActions thirdPersonPlayerActions = InputsManager.Inputs.ThirdPersonPlayer;
        
        public ThirdPersonPlayerGameState(StateMachineBase stateMachineBase, MonoBehaviour context) 
            : base(stateMachineBase, context)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            thirdPersonPlayerActions.Enable();
            thirdPersonPlayerActions.Inventory.performed += ToInventoryState;
        }

        public override void OnExit()
        {
            base.OnExit();
            thirdPersonPlayerActions.Disable();
            thirdPersonPlayerActions.Inventory.performed -= ToInventoryState;
        }

        void ToInventoryState(InputAction.CallbackContext _)
        {
            gameStateMachine.TransitionTo(GameStateMachine.State.Inventory);
        }
    }
}
