using UnityEngine;

public class Level01Trigger : MonoBehaviour
{

    public GameObject secretRoom;
    public GameObject secretWalls;
    private bool triggerActivated = false; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitialLevelSetUp(); 
    }

    

    private void InitialLevelSetUp() 
    {
        secretWalls.SetActive(true);
        secretRoom.SetActive(false);
        triggerActivated = false; 
    }

    private void TriggerLevel() 
    {
        secretWalls.SetActive(false);
        secretRoom.SetActive(true);  
        triggerActivated = true;
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !triggerActivated) 
        {
            TriggerLevel();
        }
    }
}
