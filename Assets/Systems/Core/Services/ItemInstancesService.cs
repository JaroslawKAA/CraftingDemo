using System.Collections.Generic;
using UnityEngine;

namespace Systems.Core.Services
{
    public class ItemInstancesService : IItemInstancesService
    {
        Dictionary<string, GameObject> itemInstances;
        
        public void RegisterInstances(Dictionary<string, GameObject> itemInstances)
        {
            this.itemInstances = itemInstances;
        }

        public GameObject GetItemInstance(string guid)
        {
            return GameManager.Instance.InstantiateGO(itemInstances[guid]);
        }
    }
}
