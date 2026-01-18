using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlaceObjectController : MonoBehaviour
{
    public LayerMask detectionLayers;
    private BoxCollider refCollider;
    public Color validColor = Color.green;
    public Color invalidColor = Color.red;

    private SpriteRenderer spriteRenderer;
    private bool isValidPlacement = false;
    private bool isTryingToPlace = false;
    int randomIndex;
    bool isPlayerInHidingSpot = false;

    [SerializeField] private InputManager input;
    [SerializeField] private List<GameObject> placeableObjects;
    private GameObject objectToPlace;


    private void OnEnable()
    {
        input.PlaceObjectInputEvent += TryPlaceObject;
        PlayerEvents.Instance.playerEnterHidingSpot += () => isPlayerInHidingSpot = true;
        PlayerEvents.Instance.playerExitHidingSpot += () => isPlayerInHidingSpot = false;
    }

    private void OnDisable()
    {
        input.PlaceObjectInputEvent -= TryPlaceObject;
        PlayerEvents.Instance.playerEnterHidingSpot -= () => isPlayerInHidingSpot = true;
        PlayerEvents.Instance.playerExitHidingSpot -= () => isPlayerInHidingSpot = false;
    }
    private void Start()
    {
        refCollider = GetComponent<BoxCollider>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        ValidatePlacement();
    }
    private void ValidatePlacement()
    {
        spriteRenderer.enabled = isTryingToPlace && !isPlayerInHidingSpot;
        Vector3 center = refCollider.bounds.center;
        Vector3 halfExtents = Vector3.Scale(refCollider.size, transform.lossyScale) * 0.5f;
        halfExtents.z = 3.0f;
        if (Physics.CheckBox(center, halfExtents, transform.rotation, detectionLayers))
        {
            isValidPlacement = false;
            spriteRenderer.color = invalidColor;
        }
        else
        {
            isValidPlacement = true;
            spriteRenderer.color = validColor;
        }
    }


    private void TryPlaceObject(InputAction.CallbackContext context)
    {
        if (isTryingToPlace && isValidPlacement)
        {
            Vector3 placementPosition = spriteRenderer.gameObject.transform.position;
            placementPosition.y = 0f;
            Instantiate(objectToPlace, placementPosition, Quaternion.identity);
            isTryingToPlace = false;
        }
        else if (isTryingToPlace && !isValidPlacement)
        {
            isTryingToPlace = false;
        }
        else
        {
            GetRandomPlaceable();
            isTryingToPlace = true;
        }
    }

    private void GetRandomPlaceable()
    {
        randomIndex = Random.Range(0, placeableObjects.Count);
        objectToPlace = placeableObjects[randomIndex];
    }

    private void OnDrawGizmos()
    {
        BoxCollider col = GetComponent<BoxCollider>();
        if (col == null) return;

        Gizmos.color = isValidPlacement ? Color.green : Color.red;

        Matrix4x4 rotationMatrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
        Gizmos.matrix = rotationMatrix;

        Vector3 size = col.size;
        size.z = 6.0f;

        Gizmos.DrawWireCube(col.center, size);
    }
}
