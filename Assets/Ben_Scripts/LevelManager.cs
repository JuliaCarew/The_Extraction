using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : SingletonBase<LevelManager>
{
    public GameObject player; 
    public PlayerMovement playerController;
    private string spawnPointName;

    List<LevelDataSO> levels = new List<LevelDataSO>();
    int currentLevelIndex = 0;
    int killCount = 0;

    [SerializeField] ScoreManager scoreManager;

    private void Awake()
    {
        playerController = FindFirstObjectByType<PlayerMovement>();
    }   

    public void LoadSceneWithSpawnPoint(int buildIndex, string spawnPoint)
    {
        spawnPointName = spawnPoint;

        SceneManager.sceneLoaded += onSceneLoaded;

        SceneManager.LoadScene(buildIndex);
    }

    void onSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene: " + scene.name + "is loaded");
        SetPlayerToSpawn(spawnPointName);
    }

    public void SetPlayerToSpawn(string spawnPointName)
    {
        GameObject spawnPointObject = GameObject.Find(spawnPointName);
        if (spawnPointObject != null)
        {
            if (player != null)
            {
                // Set the player position to the spawn object
                Transform spawnPointTransform = spawnPointObject.transform;
                player.transform.position = spawnPointTransform.position;
                player.transform.eulerAngles = spawnPointTransform.eulerAngles;
            }
            else
            {
                Debug.LogError("Player not found in the scene!");
            }
        }
        else
        {
            Debug.LogError($"Spawn point with {spawnPointName} not found in the scene!");
        }
    }

    public void LoadNextLevel()
    {
        LoadSceneWithSpawnPoint(currentLevelIndex, "SpawnLevel2");

    }
    public void CheckCompletion()
    {
        if (levels[currentLevelIndex].enemyCount < scoreManager.GetEnemiesKilled())
        {
            currentLevelIndex++;
            LoadNextLevel();
        }
    }


    private void OnDisable()
    {
        SceneManager.sceneLoaded -= onSceneLoaded;
    }
}
