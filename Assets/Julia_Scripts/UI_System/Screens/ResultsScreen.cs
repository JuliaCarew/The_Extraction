using TMPro;
using UnityEngine;

public class ResultsScreen : UIScreen
{
    [SerializeField] private TextMeshProUGUI teethText;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI detectionText;

    [SerializeField] private ScoreManager scoreManager;

    private void Start()
    {
        if (scoreManager == null)
        {
            Debug.LogWarning("GameplayScreen: ScoreManager not found in scene!");
        }
    }

    private void Update()
    {
        // Update UI in real-time
        if (scoreManager != null)
        {
            // Update teeth count
            if (teethText != null)
            {
                teethText.text = scoreManager.GetTeethCollected().ToString();
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