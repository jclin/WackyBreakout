using System.Collections;
using UnityEngine;

public abstract class Menu : MonoBehaviour
{
    protected abstract void Initialize();

    private void Start()
    {
        var audioSource = gameObject.AddComponent<AudioSource>();
        AudioManager.Initialize(audioSource);

        Initialize();
    }

    protected void GoToMenu(MenuName menuName)
    {
        StartCoroutine(GoToMenuCore(menuName));
    }

    protected IEnumerator PlayClickAudioCoroutine()
    {
        AudioManager.Play(AudioClipName.MenuButtonClicked);
        yield return new WaitForSeconds(0.15f);
    }

    private IEnumerator GoToMenuCore(MenuName menuName)
    {
        yield return PlayClickAudioCoroutine();

        MenuManager.GoToMenu(menuName);
    }
}
