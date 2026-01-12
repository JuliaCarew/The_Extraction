using UnityEngine;
using TMPro;

public class GameplayScreen : UIScreen
{
    [SerializeField] private TextMeshProUGUI teethText;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI detectionText;

    private StatsTracker statsTracker;
    private ScoreManager scoreManager;

    private void Start()
    {
        if (statsTracker == null)
        {
            Debug.LogWarning("GameplayScreen: StatsTracker not found in scene!");
        }
        
        if (scoreManager == null)
        {
            Debug.LogWarning("GameplayScreen: ScoreManager not found in scene!");
        }
    }

    private void Update()
    {
        // Update UI in real-time
        if (statsTracker != null && IsVisible)
        {
            // Update teeth count
            if (teethText != null)
            {
                teethText.text = statsTracker.GetTooth().ToString();
            }

            // Update money amount
            if (moneyText != null)
            {
                moneyText.text = statsTracker.GetMoney().ToString();
            }

            // Update timer 
            if (timerText != null)
            {
                float time = statsTracker.getTimer();
                int minutes = Mathf.FloorToInt(time / 60);
                int seconds = Mathf.FloorToInt(time % 60);
                timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }

            // Update detection percentage
            if (detectionText != null && scoreManager != null)
            {
                float detection = scoreManager.GetTotalDetectionPercentage();
                detectionText.text = detection.ToString("F1") + "%";
            }
        }
    }
}
