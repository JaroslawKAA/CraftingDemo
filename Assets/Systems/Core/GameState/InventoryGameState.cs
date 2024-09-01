using Systems.Core.Cursor;
using Systems.Core.InputSystem;
using UnityEngine;

namespace Systems.Core.GameState
{
    public class InventoryGameState : GameStateBase
    {
        Inputs.InventoryActions inventoryActions = InputsManager.Inputs.Inventory;
        Inputs.UiShortcutsActions uiShortcutsActions = InputsManager.Inputs.UiShortcuts;

        public InventoryGameState(StateMachineBase stateMachineBase, MonoBehaviour context) 
            : base(stateMachineBase, context)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();

            TurnOnInputs();
            CursorManager.SetCursorVisible();
            CursorManager.UnlockCursor();
        }

        public override void OnExit()
        {
            base.OnExit();

            TurnOffInputs();
        }

        void TurnOnInputs()
        {
            inventoryActions.Enable();
            uiShortcutsActions.Enable();

            uiShortcutsActions.Inventory.performed += ToThirdPersonPlayerState;
            uiShortcutsActions.Back.performed += ToThirdPersonPlayerState;
            uiShortcutsActions.Crafting.performed += ToCraftingState;
        }

        void TurnOffInputs()
        {
            inventoryActions.Disable();
            uiShortcutsActions.Disable();
            
            uiShortcutsActions.Inventory.performed -= ToThirdPersonPlayerState;
            uiShortcutsActions.Back.performed -= ToThirdPersonPlayerState;
            uiShortcutsActions.Crafting.performed -= ToCraftingState;
        }
    }
}
