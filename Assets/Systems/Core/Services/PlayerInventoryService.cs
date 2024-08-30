using System.Collections.Generic;
using Systems.Core.GameEvents;
using Systems.Core.GameEvents.Events;

namespace Systems.Core.Services
{
    public class PlayerInventoryService : IPlayerInventoryService
    {
        IReadOnlyDictionary<string, int> playerInventory;

        public IReadOnlyDictionary<string, int> PlayerInventory => playerInventory;

        public void SetPlayerInventory(IReadOnlyDictionary<string, int> playerInventory)
        {
            this.playerInventory = playerInventory;
        }

        public void AddItem(string guid)
        {
            EventManager.TriggerEvent(new AddItemToPlayerInventoryEvent(guid));
        }

        public void RemoveItem(string guid)
        {
            EventManager.TriggerEvent(new RemoveItemFromPlayerInventoryEvent(guid));
        }
    }
}
