using Systems.Core.Cursor;
using Systems.Core.InputSystem;
using UnityEngine;

namespace Systems.Core.GameState
{
    public class CraftingGameState : GameStateBase
    {
        Inputs.UiShortcutsActions uiShortcutsActions = InputsManager.Inputs.UiShortcuts;
        
        public CraftingGameState(StateMachineBase stateMachineBase, MonoBehaviour context) : base(stateMachineBase, context)
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
            uiShortcutsActions.Enable();
            
            uiShortcutsActions.Crafting.performed += ToThirdPersonPlayerState;
            uiShortcutsActions.Inventory.performed += ToInventoryState;
            uiShortcutsActions.Back.performed += ToThirdPersonPlayerState;
        }

        void TurnOffInputs()
        {
            uiShortcutsActions.Disable();
            
            uiShortcutsActions.Crafting.performed -= ToThirdPersonPlayerState;
            uiShortcutsActions.Inventory.performed -= ToInventoryState;
            uiShortcutsActions.Back.performed -= ToThirdPersonPlayerState;
        }
    }
}
