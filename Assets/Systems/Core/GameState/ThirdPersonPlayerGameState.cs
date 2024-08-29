using Systems.Core.InputSystem;
using UnityEngine;

namespace Systems.Core.GameState
{
    public class ThirdPersonPlayerGameState : GameStateBase
    {
        Inputs.ThirdPersonPlayerActions thirdPersonPlayerActions = InputsManager.Inputs.ThirdPersonPlayer;
        Inputs.UiShortcutsActions uiShortcutsActions = InputsManager.Inputs.UiShortcuts;
        
        public ThirdPersonPlayerGameState(StateMachineBase stateMachineBase, MonoBehaviour context) 
            : base(stateMachineBase, context)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            
            thirdPersonPlayerActions.Enable();
            uiShortcutsActions.Enable();
            
            uiShortcutsActions.Inventory.performed += ToInventoryState;
            uiShortcutsActions.Crafting.performed += ToCraftingState;
        }

        public override void OnExit()
        {
            base.OnExit();
            
            thirdPersonPlayerActions.Disable();
            uiShortcutsActions.Disable();
            
            uiShortcutsActions.Inventory.performed -= ToInventoryState;
            uiShortcutsActions.Crafting.performed -= ToCraftingState;
        }
    }
}
