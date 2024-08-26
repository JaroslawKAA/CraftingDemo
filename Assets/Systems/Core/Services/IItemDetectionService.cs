using System;

namespace Systems.Core.Services
{
    public interface IItemDetectionService
    {
        event Action<string> OnItemDetected;
        event Action<string> OnItemReleased;

        string SelectedItem { get; }
    }
}
