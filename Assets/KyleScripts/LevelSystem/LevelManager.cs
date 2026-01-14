using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
   [SerializeField] private ScoreManager scoreManager;
   private int currentLevelIndex = 0;
   [SerializeField] private List<LevelDataSO> levels;
    [SerializeField] private LevelLoader levelLoader;

    private void OnEnable()
    {
        EnemyEvents.Instance.OnEnemyKilled += CheckLevelCompletion;
        PlayerEvents.Instance.RoomCleared += LoadNextLevel;
    }

    private void OnDisable()
    {
        EnemyEvents.Instance.OnEnemyKilled -= CheckLevelCompletion;
        PlayerEvents.Instance.RoomCleared -= LoadNextLevel;
    }


    private void CheckLevelCompletion()
    {
        LevelDataSO currentLevel = levels[currentLevelIndex];
        Debug.Log(currentLevel.enemyCount + " enemies in level.");
        Debug.Log(scoreManager.EnemiesKilled + " enemies killed so far.");
        if (currentLevel.enemyCount == scoreManager.EnemiesKilled)
        {
            Debug.Log("Level completed!");
            currentLevel.ClearRoom();
            GameStateEvents.Instance.RaiseStateChanged(GameState.Results); // change state to results
        }
    }

    public void LoadNextLevel()
    {
        currentLevelIndex++;
        if (currentLevelIndex < levels.Count) 
        {
            LevelDataSO nextLevel = levels[currentLevelIndex];
            levelLoader.ChangeScene(nextLevel.levelName);
            scoreManager.ResetValues(); // reset data for the new level
            Debug.Log("Loading next level: " + nextLevel.levelName);
        }
        else
        {
            Debug.Log("All levels completed or index out of range.");
        }
    }


   
}
