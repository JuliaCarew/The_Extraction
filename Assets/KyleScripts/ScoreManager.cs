using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int teethCollected = 0;
    private int roomClearedTime = 0;
    private int moneyCollected = 0;
    private int stealthScore = 0;

    private int totalScore = 0;

    private void Awake()
    {
        // Subscribe to room cleared event to record time.
        // Subscribe to enemy died event, to increase stealth score.
        // Subscribe to tooth collected event.
        // Subscribe to money collected event.
    }

    private void OnDestroy()
    {
        // Unsubscribe from stuff here.
    }

    private void CollectTooth()
    {
        teethCollected++;
    }

    private void CollectMoney(int value)
    {
        moneyCollected += value;
    }

    private void EnemyDied(float stealthBonus)
    {
        stealthScore += Mathf.RoundToInt(stealthBonus);
    }

    private void RoomCleared(float timeTaken)
    {
        roomClearedTime = Mathf.RoundToInt(timeTaken);
    }

    private int CalculateTotalScore()
    {
        return teethCollected + moneyCollected + stealthScore + roomClearedTime;
    }

    public int GetTotalScore()
    {
        return CalculateTotalScore();
    }
}
