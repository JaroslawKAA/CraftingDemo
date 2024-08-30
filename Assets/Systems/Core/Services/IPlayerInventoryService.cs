using System.Collections.Generic;

namespace Systems.Core.Services
{
    public interface IPlayerInventoryService
    {
        IReadOnlyDictionary<string, int> PlayerInventory { get; }
        void SetPlayerInventory(IReadOnlyDictionary<string, int> playerInventory);
    }
}
