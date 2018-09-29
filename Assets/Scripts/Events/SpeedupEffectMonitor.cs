using UnityEngine;

public class SpeedupEffectMonitor : MonoBehaviour
{
    public bool SpedUp
    {
        get;
        private set;
    }

    public bool ExitedSpeedup
    {
        get { return speedupTimer.Finished; }
    }

    public float SecondsRemaining
    {
        get { return speedupTimer.TimeRemaining; }
    }

    public float SpeedupFactor
    {
        get;
        private set;
    }

    private Timer speedupTimer;

    private void Start()
    {
        speedupTimer = gameObject.AddComponent<Timer>();
        EventManager.AddSpeedupEffectActivatedListener(OnSpeedupEffectActivated);
    }

    private void Update()
    {
        if (speedupTimer.Finished && speedupTimer.Started)
        {
            if (SpedUp)
            {
                AudioManager.Play(AudioClipName.SpeedupEffectDeactivated);
            }

            //speedupTimer.Stop();
            SpedUp = false;
        }
    }

    private void OnSpeedupEffectActivated(float speedupDurationSeconds, float speedFactor)
    {
        AudioManager.Play(AudioClipName.SpeedupEffectActivated);

        if (speedupTimer.Running)
        {
            speedupTimer.Add(speedupDurationSeconds);
        }
        else
        {
            SpeedupFactor = speedFactor;
            speedupTimer.Duration = speedupDurationSeconds;
            speedupTimer.Run();
        }

        SpedUp = true;
    }
}