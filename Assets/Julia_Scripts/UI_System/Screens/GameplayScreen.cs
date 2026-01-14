using UnityEngine;
using TMPro;
using System.Collections;

public class GameplayScreen : UIScreen
{
    [SerializeField] private TextMeshProUGUI teethText;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI detectionText;

    //[SerializeField] private StatsTracker statsTracker;
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
        if (scoreManager != null && IsVisible)
        {
            // Update teeth count
            if (teethText != null)
            {
                teethText.text = scoreManager.GetTeethCollected().ToString();
            }

            // Update money amount
            if (moneyText != null)
            {
                moneyText.text = scoreManager.GetMoney().ToString();
            }

            /*// Update timer 
            if (timerText != null)
            {
                float time = statsTracker.getTimer();
                int minutes = Mathf.FloorToInt(time / 60);
                int seconds = Mathf.FloorToInt(time % 60);
                timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }*/

            // Update detection percentage
            if (detectionText != null && scoreManager != null)
            {
                float detection = scoreManager.GetTotalDetectionPercentage();
                detectionText.text = detection.ToString("F1") + "%";
                if (detection == scoreManager.MaxDetectionPercentage)
                {
                    StartCoroutine(ChangeToGameover());
                }
            }
        }
    }

    private IEnumerator ChangeToGameover()
    {
        yield return new WaitForSeconds(0.5f);
        GameStateMachine.Instance.ChangeState(GameState.GameOver);
    }
}