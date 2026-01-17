using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;

public class GameplayScreen : UIScreen
{
    #region Variables

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI teethText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI detectionText;

    [SerializeField] private ScoreManager scoreManager;

    private float timeCounter; 
    public float lastCheckedAwareness;

    #endregion

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        if (scoreManager == null)
        {
            Debug.LogWarning("GameplayScreen: ScoreManager not found in scene!");
        }

        timeCounter = 0;
        lastCheckedAwareness = 0f;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
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
                teethText.text = "Teeth Collected: " + scoreManager.GetTeethCollected().ToString();
            }

            if(timerText != null) 
            {
                int minutes = Mathf.FloorToInt(timeCounter / 60);
                int seconds = Mathf.FloorToInt(timeCounter % 60);
                timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
                scoreManager.UpdateRoomClearedTime(seconds);
            }

            // Update detection percentage
            if (detectionText != null && scoreManager != null)
            {
                float detection = scoreManager.GetTotalDetectionPercentage();
                //Debug.Log("Detection: " + detection);
                if (lastCheckedAwareness != detection)
                {
                    Debug.Log("Change Awareness UI"); 
                    detectionText.text = detection.ToString("F1") + "%";
                    // ONLY trigger game over if were in gameplay state
                    if (detection >= scoreManager.MaxDetectionPercentage && 
                        GameStateMachine.Instance != null && 
                        GameStateMachine.Instance.GetCurrentState() == GameState.Gameplay)
                    {
                        Debug.Log("detection is max detection percentage: " + scoreManager.MaxDetectionPercentage);
                        ChangeToGameover();
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

    private void ChangeToGameover()
    {
        timeCounter = 0;
        scoreManager.ResetValues();
        GameStateMachine.Instance.ChangeState(GameState.GameOver);
    }
}