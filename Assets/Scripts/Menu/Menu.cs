using UnityEngine;

public abstract class Menu : MonoBehaviour
{
    protected abstract void Initialize();

    private void Start()
    {
        Initialize();
    }

    protected void GoToMenu(MenuName menuName)
    {
        AudioManager.Play(AudioClipName.MenuButtonClicked);

        MenuManager.GoToMenu(menuName);
    }
}
