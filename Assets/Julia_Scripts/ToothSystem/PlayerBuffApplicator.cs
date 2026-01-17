using System.Collections.Generic;
using UnityEngine;

// applies buffs to player components
public class PlayerBuffApplicator : MonoBehaviour, IBuffApplicator
{
    [Header("Component References")]
    [SerializeField] private PlayerMovement playerMovement;

    private void Start()
    {
        if (playerMovement == null)
        {
            playerMovement = GetComponent<PlayerMovement>();
        }

        ToothBuffSystem buffSystem = GetComponent<ToothBuffSystem>();
        if (buffSystem != null)
        {
            buffSystem.RegisterBuffApplicator(this);
        }
    }

    public void ApplyBuffs(Dictionary<BuffType, float> activeBuffs)
    {
        if (activeBuffs == null)
            return;

        // Apply speed buff
        if (activeBuffs.ContainsKey(BuffType.SpeedIncrease))
        {
            ApplySpeedBuff(activeBuffs[BuffType.SpeedIncrease]);
        }

        // Apply stealth buff
        if (activeBuffs.ContainsKey(BuffType.Stealth))
        {
            ApplyStealthBuff(activeBuffs[BuffType.Stealth]);
        }

    }

    private void ApplySpeedBuff(float speedMultiplier)
    {
        if (playerMovement != null)
        {
           
            Debug.Log($"Speed buff applied: {speedMultiplier}x");
        }
    }

    private void ApplyStealthBuff(float stealthValue)
    {
        
        Debug.Log($"Stealth buff applied: {stealthValue}");
    }
}