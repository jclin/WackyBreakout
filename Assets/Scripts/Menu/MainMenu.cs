using UnityEngine;

public sealed class MainMenu : Menu
{
    protected override void Initialize()
    {
    }

    public void HandlePlayButtonOnClickEvent()
    {
        GoToMenu(MenuName.Gameplay);
    }

    public void HandleHelpButtonOnClickEvent()
    {
        GoToMenu(MenuName.Help);
    }

    public void HandleQuitButtonOnClickEvent()
    {
        AudioManager.Play(AudioClipName.MenuButtonClicked);
        Application.Quit();
    }
}