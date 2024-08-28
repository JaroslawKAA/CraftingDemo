using Systems.Core.Patterns.State;
using UnityEngine;

namespace Systems.Core.GameState
{
    public class CraftingGameState : StateBase
    {
        public CraftingGameState(StateMachineBase stateMachineBase, MonoBehaviour context) : base(stateMachineBase, context)
        {
        }
    }
}
