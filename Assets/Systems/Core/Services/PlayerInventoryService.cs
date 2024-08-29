using System.Collections.Generic;

namespace Systems.Core.Services
{
    public class PlayerInventoryService : IPlayerInventoryService
    {
        IReadOnlyDictionary<string, int> playerInventory;

        public void SetPlayerInventory(IReadOnlyDictionary<string, int> playerInventory)
        {
            this.playerInventory = playerInventory;
        }

        public IReadOnlyDictionary<string, int> PlayerInventory => playerInventory;
    }
}
