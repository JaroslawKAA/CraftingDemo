using Systems.Core.Services;
using UnityEngine;

namespace Systems.Core
{
    public class GameManager : MonoBehaviour
    {
        IItemDetectionService itemDetectionService;

        void Awake()
        {
            itemDetectionService = new ItemDetectionService();
        }
    }
}
