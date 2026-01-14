using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class GameplayScreen : UIScreen
{
    [SerializeField] private TextMeshProUGUI teethText;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI detectionText;

    //[SerializeField] private StatsTracker statsTracker;
    [SerializeField] private ScoreManager scoreManager;

    private float timeCounter; 

    public float lastCheckedAwareness;
    

    private void Start()
    {
        if (scoreManager == null)
        {
            Debug.LogWarning("GameplayScreen: ScoreManager not found in scene!");
        }

        timeCounter = 0; 

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


            if(timerText != null) 
            {
                int minutes = Mathf.FloorToInt(timeCounter / 60);
                int seconds = Mathf.FloorToInt(timeCounter % 60);
                timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds); 
            }

            // Update detection percentage
            if (detectionText != null && scoreManager != null)
            {
                float detection = scoreManager.GetTotalDetectionPercentage();
                Debug.Log("Detection: " + detection);
                if (lastCheckedAwareness != detection)
                {
                    Debug.Log("Change Awareness UI"); 
                    detectionText.text = detection.ToString("F1") + "%";
                    if (detection >= scoreManager.MaxDetectionPercentage)
                    {
                        Debug.Log("detection is max detection percentage: " + scoreManager.MaxDetectionPercentage);
                        StartCoroutine(ChangeToGameover());
                    }
                    lastCheckedAwareness = detection;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        timeCounter += Time.fixedDeltaTime; 
    }

    private IEnumerator ChangeToGameover()
    {
        yield return new WaitForSeconds(0.5f);
        GameStateMachine.Instance.ChangeState(GameState.GameOver);
    }
}