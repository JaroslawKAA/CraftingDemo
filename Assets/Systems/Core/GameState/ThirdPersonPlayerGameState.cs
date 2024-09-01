using Systems.Core.Cursor;
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

            TurnOnInputs();
            CursorManager.SetCursorNotVisible();
            CursorManager.LockCursor();
        }

        public override void OnExit()
        {
            base.OnExit();

            TurnOffInputs();
        }

        void TurnOnInputs()
        {
            thirdPersonPlayerActions.Enable();
            uiShortcutsActions.Enable();
            
            uiShortcutsActions.Inventory.performed += ToInventoryState;
            uiShortcutsActions.Crafting.performed += ToCraftingState;
        }

        void TurnOffInputs()
        {
            thirdPersonPlayerActions.Disable();
            uiShortcutsActions.Disable();
            
            uiShortcutsActions.Inventory.performed -= ToInventoryState;
            uiShortcutsActions.Crafting.performed -= ToCraftingState;
        }
    }
}
