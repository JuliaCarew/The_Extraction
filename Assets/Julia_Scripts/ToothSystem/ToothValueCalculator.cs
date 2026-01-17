using UnityEngine;

public static class ToothValueCalculator
{
    public static float CalculateToothValue(float baseValue, GameObject player)
    {
        if (player == null)
        {
            return baseValue;
        }

        ToothBuffSystem buffSystem = player.GetComponent<ToothBuffSystem>();
        if (buffSystem == null)
        {
            return baseValue;
        }

        float multiplier = buffSystem.GetBuffValue(BuffType.ToothValueMultiplier);
        return baseValue * multiplier;
    }
}