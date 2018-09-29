
public static class ConfigurationUtils
{
    public static ConfigurationData ConfigurationData;

    public static float PaddleMoveUnitsPerSecond
    {
        get { return ConfigurationData.PaddleMoveUnitsPerSecond; }
    }

    public static float BallImpulseForce
    {
        get { return ConfigurationData.BallImpulseForce; }
    }

    public static float BallLifespanSeconds
    {
        get { return ConfigurationData.BallLifespanSeconds; }
    }

    public static float MinSpawnIntervalSeconds
    {
        get { return ConfigurationData.MinSpawnIntervalSeconds; }
    }

    public static float MaxSpawnIntervalSeconds
    {
        get { return ConfigurationData.MaxSpawnIntervalSeconds; }
    }

    public static int StandardBlockPoints
    {
        get { return ConfigurationData.StandardBlockPoints; }
    }

    public static int FreezerDurationSeconds
    {
        get { return 2; }
    }

    public static int SpeedupDurationSeconds
    {
        get { return 2; }
    }

    public static float SpeedupFactor
    {
        get { return 1.5f; }
    }

    public static int TotalBalls
    {
        get { return 5; }
    }

    public static void Initialize()
    {
    }
}
