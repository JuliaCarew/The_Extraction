using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic; 

public class CameraController : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    public GameObject objCamera;
    private Vector3 initialCameraPos; 

    private List<Vector3> listCameraPositions = new List<Vector3>();
    [SerializeField] private int listIndex = 0;
    private int currentListIndex = 0; 

    private void Awake()
    {
        if (objCamera != null) {
            initialCameraPos = objCamera.transform.position; 
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SettingTheCameraPosition();
        currentListIndex = listIndex; 
    }

    // Update is called once per frame
    void Update()
    {
        if(currentListIndex != listIndex) 
        {
            listIndex = currentListIndex;
            SettingNextPosition(listCameraPositions, listIndex); 

        }
    }

    private void SettingTheCameraPosition() 
    {
        // Clear the list of the positions and adds the positions that we want

        listCameraPositions.Clear(); 

        listCameraPositions.Add(initialCameraPos);

        Vector3 camPos = objCamera.transform.position; 

        listCameraPositions.Add(AddingNewHeight(camPos, 24f));

        listCameraPositions.Add(AddingNewHeight(camPos, 16f));

        listCameraPositions.Add(AddingNewHeight(camPos, 8f));      
        
    }

    public void IncreasingIndex() 
    {
        currentListIndex++; 
        if(currentListIndex >= listCameraPositions.Capacity) 
        {
            currentListIndex = 0; 
        }
    }

    private void SettingNextPosition(List<Vector3> ListPos, int index) 
    {
       int iIndex = index;
       ChangingCameraPosition(ListPos, iIndex);          
    }

    public void ChangingCameraPosition(List<Vector3> ListPos, int index) 
    {
        objCamera.transform.position = ListPos[index]; 
    }

    private Vector3 AddingNewHeight(Vector3 pos, float height) 
    {
        return new Vector3(pos.x, height, pos.z); 
    }


    private void OnEnable()
    {
        inputManager.ChangeCameraInputEvent += IncreasingIndex; 
    }

    private void OnDisable()
    {
        inputManager.ChangeCameraInputEvent -= IncreasingIndex; 
    }



}
