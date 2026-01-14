using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : SingletonBase<LevelLoader>
{
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void ChangeScene(string sceneName)
    {
        Debug.Log("Changing scene to: " + sceneName);
        SceneManager.LoadScene(sceneName);
    }
    public void ChangeSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Transform spawnPoint = GameObject.Find("SpawnPoint")?.transform;
        GameObject player = GameObject.FindWithTag("Player");
        if (spawnPoint != null && player != null)
        {
            player.transform.position = spawnPoint.transform.position;
        }
    }

    public void LoadCurrentLevel() 
    {
        GameStateEvents.Instance.RaiseStateChanged(GameState.Gameplay); // change to gameplay state 
        // Get the build index of the currently active scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentSceneIndex); 
    }
}
