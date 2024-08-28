using Systems.Core.Patterns.State;
using UnityEngine;

namespace Systems.Core.GameState
{
    public class InventoryGameState : StateBase
    {
        public InventoryGameState(StateMachineBase stateMachineBase, MonoBehaviour context) : base(stateMachineBase, context)
        {
        }
    }
}
