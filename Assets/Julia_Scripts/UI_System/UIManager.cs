using UnityEngine;
using System.Collections.Generic;

public class UIManager : SingletonBase<UIManager>
{
    [SerializeField] private List<UIScreen> screens;

    private Dictionary<string, IUIScreen> screenDict;

    private void Awake()
    {
        base.Awake();
        
        screenDict = new Dictionary<string, IUIScreen>();
        foreach (var screen in screens)
        {
            screenDict[screen.name.ToLower()] = screen;
            screen.Hide(); // start hidden
        }
    }

    public void ShowScreen(string screenName)
    {
        if (screenDict.TryGetValue(screenName.ToLower(), out var screen))
        {
            screen.Show();
        }
        else
            Debug.LogWarning($"UIManager: No screen named {screenName}");
    }

    public void HideScreen(string screenName)
    {
        if (screenDict.TryGetValue(screenName.ToLower(), out var screen))
        {
            screen.Hide();
        }
    }

    public void HideAllScreens()
    {
        foreach (var screen in screenDict.Values)
            screen.Hide();
    }
}