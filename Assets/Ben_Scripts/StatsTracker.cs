using UnityEngine;

public class StatsTracker : MonoBehaviour
{
    private int nTooth;
    private int nMoney;
    private float nTimer;

    private void Start()
    {
        StartingStatsValues(); 
    }

    public void GetTeeth() 
    {  
        nTooth++; 
    }

    public void GetMoney(int amount) 
    {
        nMoney += amount; 
    }

    public float getTimer()
    {
        return nTimer; 
    }

    public int GetTooth()
    {
        return nTooth;
    }

    public int GetMoney()
    {
        return nMoney;
    }

    public void StartingStatsValues() 
    {
        nTooth = 0;
        nMoney = 0;
        nTimer = 0;
    }

}
