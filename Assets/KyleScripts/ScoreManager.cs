using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int teethCollected = 0;
    private float roomClearedTime = 0f;
    private int moneyCollected = 0;
    private float stealthScore = 0f;

    private void CollectTooth()
    {
        teethCollected++;
    }

    private void CollectMoney(int value)
    {
        moneyCollected += value;
    }
}
