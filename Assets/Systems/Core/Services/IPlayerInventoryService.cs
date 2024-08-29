using System.Collections.Generic;

namespace Systems.Core.Services
{
    public interface IPlayerInventoryService
    {
        void SetPlayerInventory(IReadOnlyDictionary<string, int> playerInventory);
        IReadOnlyDictionary<string, int> PlayerInventory { get; }
    }
}
