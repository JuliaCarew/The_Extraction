using UnityEngine;
using System;

public class ScoreManager : SingletonBase<ScoreManager>
{
    private int teethCollected = 0;
    private int roomClearedTime = 0;
    private int moneyCollected = 0;
    private int stealthScore = 0;
    public float totalDetectionPercentage = 0f;
    public float MaxDetectionPercentage { get { return maxDetectionPercentage;  } }
    private float maxDetectionPercentage = 100f;
    private int enemiesKilled = 0;

    private int TotalScore = 0;
    private int lastThresholdCrossed = 0; // Tracks the last 5% threshold of detection forr tooth deay

    public int totalScore
    {
        get { return TotalScore; }
        set { TotalScore = Math.Max(0, value); }
    }

    private void OnEnable()
    {
        // Subscribe to room cleared event to record time.
        // Subscribe to enemy died event, to increase stealth score.
        // Subscribe to tooth collected event.
        // Subscribe to money collected event.
        //PlayerEvents.Instance.RoomCleared += () => RoomCleared(0f);
        PlayerEvents.Instance.toothCollected += CollectTooth;
        PlayerEvents.Instance.moneyCollected += CollectMoney;
        EnemyEvents.Instance.EnemyDiedWithDetection += OnEnemyDiedWithDetection;
        EnemyEvents.Instance.OnEnemyKilled += EnemyKilled;
        EnemyEvents.Instance.OnEnemyDetectionChanged += UpdateDetection;
    }

    private void OnDisable()
    {
        // Unsubscribe from stuff here.
        //PlayerEvents.Instance.RoomCleared -= () => RoomCleared(0f);
        PlayerEvents.Instance.toothCollected -= CollectTooth;
        PlayerEvents.Instance.moneyCollected -= CollectMoney;
        EnemyEvents.Instance.EnemyDiedWithDetection -= OnEnemyDiedWithDetection;
        EnemyEvents.Instance.OnEnemyKilled -= EnemyKilled;
        EnemyEvents.Instance.OnEnemyDetectionChanged -= UpdateDetection;
    }


    private void CollectTooth()
    {
        teethCollected++;
    }

    private void RemoveTooth()
    {
        if (teethCollected > 0)
        {
            teethCollected--;
            Debug.Log($"Tooth lost due to detection! Remaining teeth: {teethCollected}");
        }
    }

    public int GetTeethCollected()
    {
        return teethCollected;
    }

    public int GetMoney()
    {
        return moneyCollected;
    }
    private void EnemyKilled()
    {
        enemiesKilled++;
    }

    private void UpdateDetection(float awareness)
    {
        totalDetectionPercentage += awareness;
        Debug.Log("Total detection updated. now: " + totalDetectionPercentage);
        
        // Check if we've crossed a new 5% threshold
        CheckForToothLoss();
    }
    
    private void CheckForToothLoss()
    {
        // Calculate for the 5% threshold crossed
        int currentThreshold = Mathf.FloorToInt(totalDetectionPercentage / 5f);
        
        // If a new threshold is crossed, remove a tooth
        if (currentThreshold > lastThresholdCrossed)
        {
            int thresholdsCrossed = currentThreshold - lastThresholdCrossed;
            lastThresholdCrossed = currentThreshold;
            
            // Remove one tooth for each threshold crossed 
            for (int i = 0; i < thresholdsCrossed; i++)
            {
                RemoveTooth();
            }
        }
    }

    public void UpdateRoomClearedTime(int time)
    {
        roomClearedTime = time;
    }

    private void CollectMoney()
    {
        moneyCollected++;
    }

    private void EnemyDied(float stealthBonus)
    {
        stealthScore += Mathf.RoundToInt(stealthBonus);
    }

    private void OnEnemyDiedWithDetection(float detectionLevel)
    {
        totalDetectionPercentage += detectionLevel;
        // Check for tooth loss when detection changes from enemy death
        CheckForToothLoss();
    }

    private void RoomCleared(float timeTaken)
    {
        roomClearedTime = Mathf.RoundToInt(timeTaken);
    }

    public int GetEnemiesKilled()
    {
        return enemiesKilled;
    }

    private int CalculateTotalScore()
    {
        return (teethCollected * 50) - roomClearedTime - Mathf.RoundToInt(totalDetectionPercentage);
    }

    public int GetTotalScore()
    {
        return CalculateTotalScore();
    }

    public float GetTotalDetectionPercentage()
    {
        return totalDetectionPercentage;
    }

    public int GetRoomClearedTime()
    {
        return roomClearedTime;
    }

    public void ResetValues() 
    {
        totalDetectionPercentage = 0;
        teethCollected = 0;
        moneyCollected = 0;
        stealthScore = 0;
        enemiesKilled = 0;
        lastThresholdCrossed = 0;
    }
}
