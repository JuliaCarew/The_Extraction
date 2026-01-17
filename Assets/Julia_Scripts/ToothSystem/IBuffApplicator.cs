using System.Collections.Generic;

public interface IBuffApplicator
{
    // Applies buffs to the player based on the active buff values
    void ApplyBuffs(Dictionary<BuffType, float> activeBuffs);
}

