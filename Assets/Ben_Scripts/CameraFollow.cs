using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject followObject;

    private float posX;
    private float posZ; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x != followObject.transform.position.x || 
            transform.position.z != followObject.transform.position.z) 
        {
            transform.position = new Vector3(followObject.transform.position.x, transform.position.y, followObject.transform.position.z); 
        }
    }



}
