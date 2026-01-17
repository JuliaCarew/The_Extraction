using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : SingletonBase<LevelLoader>
{
    [SerializeField] private ScoreManager scoreManager;
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
        Transform spawnPoint = GameObject.Find("PlayerSpawnpoint")?.transform;
        GameObject player = GameObject.FindWithTag("Player");
        if (spawnPoint != null && player != null)
        {
            player.transform.position = spawnPoint.transform.position;
        }
        scoreManager.ResetValues(); // reset data for the new level
    }

    public void LoadCurrentLevel() 
    {
        GameStateMachine.Instance.ChangeState(GameState.Gameplay); // change to gameplay state 
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentSceneIndex); 
    }
}
