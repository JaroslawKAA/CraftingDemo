using System.Collections.Generic;
using UnityEngine;

namespace Systems.Core.Services
{
    public interface IItemInstancesService
    {
        public void RegisterInstances(Dictionary<string, GameObject> itemInstances);
        GameObject GetItemInstance(string guid);
    }
}
