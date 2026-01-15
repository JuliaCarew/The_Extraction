using TMPro;
using UnityEngine;

public class ResultsScreen : UIScreen
{
    char grade = 'F';
    private int totalScore = 0;
    [SerializeField] private TextMeshProUGUI gradeText;
    [SerializeField] private TextMeshProUGUI totalScoreText;
    [SerializeField] private TextMeshProUGUI clearTimeText;
    [SerializeField] private TextMeshProUGUI teethText;
    [SerializeField] private TextMeshProUGUI detectionText;

    [SerializeField] private ScoreManager scoreManager;

    private void OnEnable()
    {
        if (scoreManager == null)
        {
            scoreManager = FindFirstObjectByType<ScoreManager>();
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
                int teethCollected = scoreManager.GetTeethCollected();
                teethText.text = "Teeth Collected: " + teethCollected.ToString();
            }

            // Update detection percentage
            if (detectionText != null && scoreManager != null)
            {
                float detection = scoreManager.GetTotalDetectionPercentage();
                detectionText.text = "Enemy Awareness: " + detection.ToString("F1") + "%";
            }
            if (clearTimeText != null)
            {
                float clearTime = scoreManager.GetRoomClearedTime();
                clearTimeText.text = "Time taken: " + clearTime.ToString("F2") + "s";
            }
            if (totalScoreText != null)
            {
                totalScore = scoreManager.GetTotalScore();
                totalScoreText.text = "Total Score: " + totalScore.ToString();
            }
            CalculateGrade();
        }
    }

    private void CalculateGrade()
    {
        if (totalScore >= 90) grade = 'S';
        else if (totalScore >= 80) grade = 'A';
        else if (totalScore >= 70) grade = 'B';
        else if (totalScore >= 60) grade = 'C';
        else if (totalScore >= 50) grade = 'D';
        gradeText.text = "Grade: " + grade;
    }
}