using TMPro;
using UnityEngine;

public class ResultsScreen : UIScreen
{
    [SerializeField] private TextMeshProUGUI teethText;
    [SerializeField] private TextMeshProUGUI moneyText;
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

            // Update detection percentage
            if (detectionText != null && scoreManager != null)
            {
                float detection = scoreManager.GetTotalDetectionPercentage();
                detectionText.text = detection.ToString("F1") + "%";
            }
        }
    }
}