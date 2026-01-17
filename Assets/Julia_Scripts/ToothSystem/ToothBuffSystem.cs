using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Orchestrates the buff system - coordinates between buff manager and applicators
public class ToothBuffSystem : MonoBehaviour
{
    [Header("Buff Configuration")]
    [SerializeField] private List<ToothBuffData> buffConfigurations = new List<ToothBuffData>();

    private ToothBuffManager buffManager;
    private IBuffValueProvider valueProvider;
    private List<IBuffApplicator> buffApplicators = new List<IBuffApplicator>();
    private Dictionary<BuffType, float> currentActiveBuffs = new Dictionary<BuffType, float>();

    private void Start()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        // Create default value provider
        valueProvider = new DefaultBuffValueProvider();

        InitializeDefaultBuffs();

        buffManager = new ToothBuffManager(valueProvider, buffConfigurations);

        buffApplicators = GetComponents<IBuffApplicator>().ToList();

        if (buffApplicators.Count == 0)
        {
            Debug.LogWarning($"No IBuffApplicator components found on {gameObject.name}. Buffs won't be applied automatically.");
        }
    }

    private void InitializeDefaultBuffs()
    {
        if (buffConfigurations.Count == 0)
        {
            buffConfigurations.Add(new ToothBuffData
            {
                toothType = ToothType.Molar,
                requiredCount = 1,
                buffType = BuffType.ToothValueMultiplier,
                buffValue = 1.5f
            });
            
        }
    }

    // Updates buffs based on current tooth counts
    public void UpdateBuffs(Dictionary<ToothType, int> toothCounts)
    {
        if (buffManager == null)
        {
            Debug.LogError("BuffManager not initialized!");
            return;
        }

        currentActiveBuffs = buffManager.UpdateBuffs(toothCounts);

        ApplyBuffsToPlayer();
    }

    private void ApplyBuffsToPlayer()
    {
        // Apply buffs using all registered applicators
        foreach (var applicator in buffApplicators)
        {
            applicator.ApplyBuffs(currentActiveBuffs);
        }
    }

    // Gets the current value of a buff type
    public float GetBuffValue(BuffType buffType)
    {
        return buffManager?.GetBuffValue(buffType) ?? valueProvider?.GetDefaultValue(buffType) ?? 1.0f;
    }

    public bool HasBuff(BuffType buffType)
    {
        return buffManager?.HasBuff(buffType) ?? false;
    }

    // Registers a buff applicator to receive buff updates
    public void RegisterBuffApplicator(IBuffApplicator applicator)
    {
        if (applicator != null && !buffApplicators.Contains(applicator))
        {
            buffApplicators.Add(applicator);
        }
    }

    public void UnregisterBuffApplicator(IBuffApplicator applicator)
    {
        buffApplicators.Remove(applicator);
    }
}