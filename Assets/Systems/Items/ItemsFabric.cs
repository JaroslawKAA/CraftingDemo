using System.Collections.Generic;
using Systems.Core.Services;
using UnityEngine;

namespace Systems.Items
{
    public class ItemsFabric : MonoBehaviour
    {
        Dictionary<string, GameObject> itemsPrefabs = new();

        void Awake()
        {
            LoadPrefabs();
            ServicesManager.ItemInstancesService.RegisterInstances(itemsPrefabs);
        }

        public GameObject GetItemInstance(string guid)
        {
            return Instantiate(itemsPrefabs[guid]);
        }

        void LoadPrefabs()
        {
            ItemInstance[] itemInstances = Resources.LoadAll<ItemInstance>("Items");
            
            foreach (ItemInstance itemInstance in itemInstances) 
                itemsPrefabs.Add(itemInstance.Item.Guid, itemInstance.gameObject);
        }
    }
}
