using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIButtonHandler : MonoBehaviour
{
    [SerializeField] private MonoBehaviour actionComponent; // must implement IUIButtonAction

    private IUIButtonAction action;

    private void Awake()
    {
        action = actionComponent as IUIButtonAction;
        if (action == null)
            Debug.LogError($"{name}: actionComponent does not implement IUIButtonAction");

        GetComponent<Button>().onClick.AddListener(() =>
        {
            action?.OnButtonPressed();
        });
    }
}