using UnityEngine;
using System;

public class ScoreManager : MonoBehaviour
{
    private int teethCollected = 0;
    private int roomClearedTime = 0;
    private int moneyCollected = 0;
    private int stealthScore = 0;
    private float totalDetectionPercentage = 0f;
    public float MaxDetectionPercentage { get { return maxDetectionPercentage;  } }
    private float maxDetectionPercentage = 100f;
    public int EnemiesKilled { get { return enemiesKilled; } }
    private int enemiesKilled = 0;

    private int TotalScore = 0;

    public int totalScore
    {
        get { return TotalScore; }
        set { TotalScore = Math.Max(0, value); }
    }

    private void Awake()
    {
        // Subscribe to room cleared event to record time.
        // Subscribe to enemy died event, to increase stealth score.
        // Subscribe to tooth collected event.
        // Subscribe to money collected event.
        PlayerEvents.Instance.RoomCleared += () => RoomCleared(0f);
        PlayerEvents.Instance.toothCollected += CollectTooth;
        PlayerEvents.Instance.moneyCollected += CollectMoney;
        EnemyEvents.Instance.EnemyDiedWithDetection += OnEnemyDiedWithDetection;
        EnemyEvents.Instance.OnEnemyKilled += EnemyKilled;
    }

    private void OnDestroy()
    {
        // Unsubscribe from stuff here.
        PlayerEvents.Instance.RoomCleared -= () => RoomCleared(0f);
        PlayerEvents.Instance.toothCollected -= CollectTooth;
        PlayerEvents.Instance.moneyCollected -= CollectMoney;
        EnemyEvents.Instance.EnemyDiedWithDetection -= OnEnemyDiedWithDetection;
        EnemyEvents.Instance.OnEnemyKilled -= EnemyKilled;
    }

    private void CollectTooth()
    {
        teethCollected++;
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
        return teethCollected + moneyCollected + stealthScore + roomClearedTime;
    }

    public int GetTotalScore()
    {
        return CalculateTotalScore();
    }

    public float GetTotalDetectionPercentage()
    {
        return totalDetectionPercentage;
    }
}
