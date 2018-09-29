using UnityEngine;

public sealed class GameAudioSource : MonoBehaviour
{
    private void Awake()
    {
        // initialize audio manager
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        AudioManager.Initialize(audioSource);
    }
}
