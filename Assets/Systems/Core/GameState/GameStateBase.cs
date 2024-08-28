using Systems.Core.Patterns.State;
using UnityEngine;

namespace Systems.Core.GameState
{
    public abstract class GameStateBase : StateBase
    {
        protected GameStateMachine gameStateMachine;
        
        protected GameStateBase(StateMachineBase stateMachineBase, MonoBehaviour context) : base(stateMachineBase, context)
        {
            gameStateMachine = stateMachineBase as GameStateMachine;
        }
    }
}
