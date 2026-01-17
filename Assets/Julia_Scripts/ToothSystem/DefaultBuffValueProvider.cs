// Provides default values for buff types
public class DefaultBuffValueProvider : IBuffValueProvider
{
    public float GetDefaultValue(BuffType buffType)
    {
        switch (buffType)
        {
            case BuffType.SpeedIncrease:
                return 1.0f; 
            case BuffType.Stealth:
                return 1.0f; 
            case BuffType.ToothValueMultiplier:
                return 1.0f; 
            default:
                return 1.0f;
        }
    }
}