using System.Collections.Generic;
using Systems.Core.GameEvents;
using Systems.Core.GameEvents.Events;
using UnityEngine;

namespace Systems.Crafting
{
    public class CraftingEngine : MonoBehaviour
    {
        const int MinRandomChance = 0;
        const int MaxRandomChanceExclusive = 101;
        
        // PRIVATE
        Dictionary<string, CraftingRecipe> quickRecipeAccess = new();

        EventListener craftRequestListener;

        // UNITY EVENTS
        void Awake()
        {
            LoadRecipes();
            SubscribeEvents();
        }

        void OnDestroy()
        {
            UnsubscribeEvents();
        }

        // METHODS
        void LoadRecipes()
        {
            CraftingRecipe[] allRecipes = Resources.LoadAll<CraftingRecipe>("Recipes");
            foreach (CraftingRecipe craftingRecipe in allRecipes)
            {
                string recipeKey = craftingRecipe.firstItem.Guid + craftingRecipe.secondItem.Guid;
                quickRecipeAccess.Add(recipeKey, craftingRecipe);

                string invertedRecipeKey = craftingRecipe.secondItem.Guid + craftingRecipe.firstItem.Guid;
                if(!quickRecipeAccess.ContainsKey(invertedRecipeKey))
                    quickRecipeAccess.Add(invertedRecipeKey, craftingRecipe);
            }
        }

        void SubscribeEvents()
        {
            craftRequestListener = new EventListener(OnCraftRequested);
            EventManager.RegisterListener<CraftRequestEvent>(craftRequestListener);
        }

        void UnsubscribeEvents()
        {
            EventManager.UnregisterListener<CraftRequestEvent>(craftRequestListener);
            craftRequestListener = null;
        }

        void OnCraftRequested(EventBase eventBase)
        {
            CraftRequestEvent craftRequestEvent = eventBase as CraftRequestEvent;
            if (TryGetRecipe(craftRequestEvent, out CraftingRecipe craftingRecipe))
            {
                int rnd = Random.Range(MinRandomChance, MaxRandomChanceExclusive);
                if (craftingRecipe.successChance >= rnd)
                {
                    craftingRecipe.onCraftingSuccess?.Invoke();
                    TriggerConsumingItems(craftingRecipe.firstItem.Guid, craftingRecipe.secondItem.Guid);
                    EventManager.TriggerEvent(new AddItemToPlayerInventoryEvent(craftingRecipe.result.Guid));
                    EventManager.TriggerEvent(new CraftingCompletedEvent(craftingRecipe.result.Guid));
                    Debug.Log("Crafting success");
                }
                else
                {
                    Debug.Log("Crafting failed");
                    craftingRecipe.onCraftingFailed?.Invoke();
                    TriggerConsumingItems(craftingRecipe.firstItem.Guid, craftingRecipe.secondItem.Guid);
                    EventManager.TriggerEvent(new CraftingCompletedEvent(null));
                }
            }
            else
            {
                Debug.Log("No crafting recipe for chosen items");
                TriggerConsumingItems(craftRequestEvent.FirstItemGuid, craftRequestEvent.SecondItemGuid);
                EventManager.TriggerEvent(new CraftingCompletedEvent(null));
            }
        }

        static void TriggerConsumingItems(params string[] itemsGuids)
        {
            foreach (string itemsGuid in itemsGuids) 
                EventManager.TriggerEvent(new RemoveItemFromPlayerInventoryEvent(itemsGuid));
        }

        bool TryGetRecipe(CraftRequestEvent craftRequestEvent, out CraftingRecipe craftingRecipe)
        {
            string key = craftRequestEvent.FirstItemGuid + craftRequestEvent.SecondItemGuid;
            craftingRecipe = null;
            return quickRecipeAccess.TryGetValue(key, out craftingRecipe);
        }
    }
}