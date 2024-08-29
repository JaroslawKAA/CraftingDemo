using Systems.Core.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Systems.Core.GameState
{
    public class InventoryGameState : GameStateBase
    {
        Inputs.InventoryActions inventoryActions = InputsManager.Inputs.Inventory;
        
        public InventoryGameState(StateMachineBase stateMachineBase, MonoBehaviour context) 
            : base(stateMachineBase, context)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            inventoryActions.Enable();
            inventoryActions.CloseInventory.performed += ToThirdPersonPlayerState;
        }

        public override void OnExit()
        {
            base.OnExit();
            inventoryActions.Disable();
            inventoryActions.CloseInventory.performed -= ToThirdPersonPlayerState;
        }

        void ToThirdPersonPlayerState(InputAction.CallbackContext _)
        {
            gameStateMachine.TransitionTo(GameStateMachine.State.ThirdPersonPlayer);
        }
    }
}
