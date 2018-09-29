using UnityEngine;

public class Timer : MonoBehaviour
{
    private float totalSeconds;
    private float elapsedSeconds;
    private bool started;

    public Timer()
    {
        Running = false;
    }

    public float Duration
    {
        set
        {
            if (!Running) totalSeconds = value;
        }
    }

    public bool Finished
    {
        get { return started && !Running; }
    }

    public bool Started
    {
        get { return started; }
    }

    public bool Running { get; private set; }
    public float TimeRemaining { get; private set; }

    private void Update()
    {
        // update timer and check for finished
        if (Running)
        {
            elapsedSeconds += Time.deltaTime;
            TimeRemaining = totalSeconds - elapsedSeconds;
            if (elapsedSeconds >= totalSeconds) Running = false;
        }
    }

    public void Run()
    {
        if (totalSeconds > 0)
        {
            started = true;
            Running = true;
            elapsedSeconds = 0;
        }
    }

    public void Stop()
    {
        started = false;
        Running = false;
        elapsedSeconds = 0;
    }

    public void Add(float durationSeconds)
    {
        totalSeconds += durationSeconds;
    }
}