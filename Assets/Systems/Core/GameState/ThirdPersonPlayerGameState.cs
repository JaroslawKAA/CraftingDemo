using Systems.Core.InputSystem;
using UnityEngine;

namespace Systems.Core.GameState
{
    public class ThirdPersonPlayerGameState : GameStateBase
    {
        public ThirdPersonPlayerGameState(StateMachineBase stateMachineBase, MonoBehaviour context) : base(stateMachineBase, context)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            InputsManager.Inputs.ThirdPersonPlayer.Enable();
        }

        public override void OnExit()
        {
            base.OnExit();
            InputsManager.Inputs.ThirdPersonPlayer.Disable();
        }
    }
}
