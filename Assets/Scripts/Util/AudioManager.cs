using System.Collections.Generic;
using UnityEngine;

public static class AudioManager
{
    static AudioSource audioSource;
    static Dictionary<AudioClipName, AudioClip> audioClips = new Dictionary<AudioClipName, AudioClip>();

    public static void Initialize(AudioSource source)
    {
        audioSource = source;

        AddClip(AudioClipName.BallCollidedWithBall,   "BallCollidedWithBall");
        AddClip(AudioClipName.BallCollidedWithPaddle, "BallCollidedWithPaddle");
        AddClip(AudioClipName.BallCollidedWithBlock,  "BallCollidedWithBlock");
        AddClip(AudioClipName.BallSpawned,            "BallSpawned");
        AddClip(AudioClipName.BallLost,               "BallLost");

        AddClip(AudioClipName.FreezerEffectActivated, "FreezerEffectActivated");
        AddClip(AudioClipName.FreezerEffectDeactivated, "FreezerEffectDeactivated");

        AddClip(AudioClipName.SpeedupEffectActivated, "SpeedupEffectActivated");
        AddClip(AudioClipName.SpeedupEffectDeactivated, "SpeedupEffectDeactivated");

        AddClip(AudioClipName.GameLost, "GameLost");
        AddClip(AudioClipName.GameWon, "GameWon");

        AddClip(AudioClipName.MenuButtonClicked, "MenuButtonClicked");
    }

    public static void Play(AudioClipName name)
    {
        audioSource.PlayOneShot(audioClips[name]);
    }

    private static void AddClip(AudioClipName audioClipName, string audioResourceName)
    {
        if (!audioClips.ContainsKey(audioClipName))
        {
            audioClips.Add(audioClipName, Resources.Load<AudioClip>(audioResourceName));
        }
    }
}
