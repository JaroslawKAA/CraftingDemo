using System;

namespace Systems.Core.Services
{
    public class ItemDetectionService : IItemDetectionService
    {
        public event Action<string> OnItemDetected;
        public event Action<string> OnItemReleased;
        public string SelectedItem { get; }
    }
}
