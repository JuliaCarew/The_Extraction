using UnityEngine;

public class PlayerData : MonoBehaviour
{
    // Variables

    [SerializeField] private int playerScore; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartingValues();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartingValues() 
    {
        playerScore = 0;
    }
}
