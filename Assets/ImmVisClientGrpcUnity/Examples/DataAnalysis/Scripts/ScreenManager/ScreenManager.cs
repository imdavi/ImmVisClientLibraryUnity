using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    [SerializeField]
    private BaseScreen DefaultScreen;

    private Dictionary<string, BaseScreen> screensDictionary;

    private Stack<BaseScreen> screensStack;

    private BaseScreen CurrentScreen
    {
        get
        {
            try
            {
                return screensStack.Peek();
            }
            catch (System.Exception)
            {
                return null;
            }
        }
    }

    void Awake()
    {
        screensStack = new Stack<BaseScreen>();

        screensDictionary = new Dictionary<string, BaseScreen>();

        foreach (var screen in GetComponentsInChildren<BaseScreen>(true))
        {
            screensDictionary[screen.name] = screen;
        }

        if (DefaultScreen != null)
        {
            ShowScreen(DefaultScreen);
        }
    }

    private void HideScreen(BaseScreen screen)
    {
        if (screen != null)
        {
            screen.Hide();
        }
    }

    public void HideCurrentScreen()
    {
        HideScreen(CurrentScreen);
    }

    public void HideAllScreens()
    {
        foreach (var screen in screensDictionary.Values)
        {
            HideScreen(screen);
        }
    }

    public void HideScreen(string screenName)
    {
        if (!screensDictionary.ContainsKey(screenName))
        {
            Debug.LogError($"Screen not found: {screenName}");
            return;
        }

        HideScreen(screensDictionary[screenName]);
    }

    public void ShowScreen(string screenName)
    {
        ShowScreen(screenName, null);
    }

    public void ShowScreen(string screenName, object data)
    {
        if (!screensDictionary.ContainsKey(screenName))
        {
            Debug.LogError($"Screen not found: {screenName}");
            return;
        }

        ShowScreen(screensDictionary[screenName], data);
    }

    public void ShowCurrentScreen()
    {
        ShowScreen(CurrentScreen);
    }

    private void ShowScreen(BaseScreen screen, object data = null)
    {
        HideCurrentScreen();

        if (CurrentScreen?.name != screen.name)
        {
            screensStack.Push(screen);
        }

        screen.Show(data);
    }

    public void NavigateBack()
    {
        HideCurrentScreen();

        do
        {
            screensStack.Pop();
        } while (screensStack.Count > 2 && screensStack.Peek().IsPopup);

        ShowCurrentScreen();
    }

    internal void ClearNavigationStack()
    {
        HideAllScreens();
        screensStack.Clear();
    }
}
