using UnityEngine;

public sealed class GameAudioSource : MonoBehaviour
{
    private void Awake()
    {
        if (AudioManager.Initialized)
        {
            Destroy(gameObject);
            return;
        }

        var audioSource = gameObject.AddComponent<AudioSource>();
        AudioManager.Initialize(audioSource);
        DontDestroyOnLoad(gameObject);
    }
}
