using System.Collections.Generic;
using UnityEngine;

public class ToothInventory : MonoBehaviour
{
    [Header("Inventory Settings")]
    [SerializeField] private int maxToothCapacity = 3;
    
    private Dictionary<ToothType, int> toothCounts = new Dictionary<ToothType, int>();
    private List<ToothType> toothCollection = new List<ToothType>();
    private ToothBuffSystem buffSystem;

    public int MaxCapacity => maxToothCapacity;
    public int CurrentCount => toothCollection.Count;

    private void Start()
    {
        buffSystem = GetComponent<ToothBuffSystem>();
        if (buffSystem == null)
        {
            buffSystem = gameObject.AddComponent<ToothBuffSystem>();
        }
        
        InitializeToothCounts();
    }

    private void InitializeToothCounts()
    {
        foreach (ToothType type in System.Enum.GetValues(typeof(ToothType)))
        {
            toothCounts[type] = 0;
        }
    }

    public bool TryAddTooth(ToothType toothType)
    {
        if (toothCollection.Count >= maxToothCapacity)
        {
            Debug.Log($"Cannot add tooth: Inventory full ({toothCollection.Count}/{maxToothCapacity})");
            return false;
        }

        toothCollection.Add(toothType);
        toothCounts[toothType]++;
        
        PlayerEvents.Instance.ToothCollected();
        PlayerEvents.Instance.InventoryChanged();
        
        // Update buff system
        if (buffSystem != null)
        {
            buffSystem.UpdateBuffs(toothCounts);
        }
        
        Debug.Log($"Added {toothType}. Inventory: {toothCollection.Count}/{maxToothCapacity}");
        return true;
    }

    public bool RemoveTooth(ToothType toothType)
    {
        if (toothCounts[toothType] <= 0)
        {
            return false;
        }

        toothCollection.Remove(toothType);
        toothCounts[toothType]--;
        
        PlayerEvents.Instance.InventoryChanged();
        
        // Update buff system
        if (buffSystem != null)
        {
            buffSystem.UpdateBuffs(toothCounts);
        }
        
        return true;
    }

    public bool IsFull()
    {
        return toothCollection.Count >= maxToothCapacity;
    }
}