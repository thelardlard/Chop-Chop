
using UnityEngine;
using System.Collections.Generic;
using System;

public class PlayerInventory : MonoBehaviour
{
    public Dictionary<ResourceType, int> resources = new();
    public static Action OnInventoryChanged; // Event for UI

    private void Awake()
    {
        foreach (ResourceType type in System.Enum.GetValues(typeof(ResourceType)))
        {
            resources[type] = 0;
        }
             
        TriggerUpdate(); // Initial UI update
    }

    public bool HasEnough(RepairableData data)
    {
        return resources[ResourceType.Wood] >= data.woodCost &&
               resources[ResourceType.Fabric] >= data.fabricCost &&
               resources[ResourceType.Glass] >= data.glassCost;
    }

    public void SpendResources(RepairableData data)
    {
        resources[ResourceType.Wood] -= data.woodCost;
        resources[ResourceType.Fabric] -= data.fabricCost;
        resources[ResourceType.Glass] -= data.glassCost;
        TriggerUpdate();
    }

    public void AddResource(ResourceType type, int amount)
    {
        resources[type] += amount;
        TriggerUpdate();
    }

    public int GetCount(ResourceType type)
    {
        return resources[type];
    }
    private void TriggerUpdate()
    {
        OnInventoryChanged?.Invoke();
    }
}
