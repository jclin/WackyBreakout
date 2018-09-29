using UnityEngine;

public static class EffectUtils
{
    public static bool SpeedupActive
    {
        get { return Camera.main.GetComponent<SpeedupEffectMonitor>().SpedUp; }
    }

    public static bool ExitedSpeedup
    {
        get { return Camera.main.GetComponent<SpeedupEffectMonitor>().ExitedSpeedup; }
    }

    public static float SecondsRemaining
    {
        get { return Camera.main.GetComponent<SpeedupEffectMonitor>().SecondsRemaining; }
    }

    public static float SpeedupFactor
    {
        get { return Camera.main.GetComponent<SpeedupEffectMonitor>().SpeedupFactor; }
    }

    //private static float? targetSpeed;
    //public static float? TargetSpeed
    //{
    //    get { return targetSpeed;}
    //    set
    //    {
    //        targetSpeed = value;
    //        Debug.Log(string.Format("Target speed: {0}", value));
    //    }
    //}
}
