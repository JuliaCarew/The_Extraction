using System.Collections.Generic;
using UnityEngine;

// Manages buff calculations and active buff state
public class ToothBuffManager
{
    private readonly IBuffValueProvider valueProvider;
    private readonly List<ToothBuffData> buffConfigurations;
    private readonly Dictionary<BuffType, float> activeBuffs;

    public ToothBuffManager(IBuffValueProvider valueProvider, List<ToothBuffData> configurations)
    {
        this.valueProvider = valueProvider;
        this.buffConfigurations = configurations;
        this.activeBuffs = new Dictionary<BuffType, float>();
        InitializeBuffDictionary();
    }

    private void InitializeBuffDictionary()
    {
        foreach (BuffType type in System.Enum.GetValues(typeof(BuffType)))
        {
            if (type != BuffType.None)
            {
                activeBuffs[type] = valueProvider.GetDefaultValue(type);
            }
        }
    }

    // Updates active buffs based on current tooth counts
    public Dictionary<BuffType, float> UpdateBuffs(Dictionary<ToothType, int> toothCounts)
    {
        // Reset all buffs to default
        foreach (BuffType type in activeBuffs.Keys)
        {
            activeBuffs[type] = valueProvider.GetDefaultValue(type);
        }

        // Apply buffs based on tooth counts
        foreach (var buffConfig in buffConfigurations)
        {
            int currentCount = toothCounts.ContainsKey(buffConfig.toothType)
                ? toothCounts[buffConfig.toothType]
                : 0;

            if (currentCount >= buffConfig.requiredCount)
            {
                ApplyBuff(buffConfig.buffType, buffConfig.buffValue);
            }
        }

        return new Dictionary<BuffType, float>(activeBuffs);
    }

    private void ApplyBuff(BuffType buffType, float value)
    {
        switch (buffType)
        {
            case BuffType.SpeedIncrease:
                activeBuffs[BuffType.SpeedIncrease] = Mathf.Max(activeBuffs[BuffType.SpeedIncrease], value);
                break;
            case BuffType.Stealth:
                // Lower value = better stealth (harder to detect)
                activeBuffs[BuffType.Stealth] = Mathf.Min(activeBuffs[BuffType.Stealth], value);
                break;
            case BuffType.ToothValueMultiplier:
                activeBuffs[BuffType.ToothValueMultiplier] = Mathf.Max(activeBuffs[BuffType.ToothValueMultiplier], value);
                break;
        }
    }

    // Gets the current value of a buff type
    public float GetBuffValue(BuffType buffType)
    {
        return activeBuffs.ContainsKey(buffType) ? activeBuffs[buffType] : valueProvider.GetDefaultValue(buffType);
    }

    // Checks if a buff is currently active
    public bool HasBuff(BuffType buffType)
    {
        if (!activeBuffs.ContainsKey(buffType))
            return false;

        float currentValue = activeBuffs[buffType];
        float defaultValue = valueProvider.GetDefaultValue(buffType);

        return buffType == BuffType.Stealth
            ? currentValue < defaultValue
            : currentValue > defaultValue;
    }
}