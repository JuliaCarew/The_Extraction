using UnityEngine;

[System.Serializable]
public class ToothBuffData
{
    [Tooltip("The type of tooth required for this buff")]
    public ToothType toothType;
    
    [Tooltip("How many of this tooth type are needed to activate the buff")]
    public int requiredCount;
    
    [Tooltip("The type of buff to apply")]
    public BuffType buffType;
    
    [Tooltip("The value of the buff to apply")]
    public float buffValue;
}