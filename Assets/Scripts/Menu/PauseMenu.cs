using UnityEngine;

public sealed class PauseMenu : Menu
{
    protected override void Initialize()
    {
        // pause the game when added to the scene
        Time.timeScale = 0;
    }

    public void HandleResumeButtonOnClickEvent()
    {
        // unpause game and destroy menu
        Time.timeScale = 1;
        Destroy(gameObject);
    }

    public void HandleQuitButtonOnClickEvent()
    {
        // unpause game, destroy menu, and go to main menu
        Time.timeScale = 1;
        Destroy(gameObject);
        GoToMenu(MenuName.Main);
    }
}