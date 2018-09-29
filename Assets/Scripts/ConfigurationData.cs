using System;
using System.IO;
using UnityEngine;

/// <summary>
/// A container for the configuration data
/// </summary>
public class ConfigurationData
{
    private const string ConfigurationDataFileName    = "ConfigurationData.csv";

    private const int DefaultPaddleMoveUnitsPerSecond  = 10;
    private const float DefaultBallImpulseForce        = 6.5f;
    private const float DefaultBallLifespanSeconds     = 10f;
    private const float DefaultMinSpawnIntervalSeconds = 5f;
    private const float DefaultMaxSpawnIntervalSeconds = 10f;
    private const int DefaultStandardBlockPoints       = 1;

    private static float paddleMoveUnitsPerSecond = DefaultPaddleMoveUnitsPerSecond;
    private static float ballImpulseForce         = DefaultBallImpulseForce;
    private static float ballLifespanSeconds      = DefaultBallLifespanSeconds;
    private static float minSpawnIntervalSeconds  = DefaultMinSpawnIntervalSeconds;
    private static float maxSpawnIntervalSeconds  = DefaultMaxSpawnIntervalSeconds;
    private static int standardBlockPoints        = DefaultStandardBlockPoints;

    public float PaddleMoveUnitsPerSecond
    {
        get { return paddleMoveUnitsPerSecond; }
    }

    public float BallImpulseForce
    {
        get { return ballImpulseForce; }
    }

    public float BallLifespanSeconds
    {
        get { return ballLifespanSeconds; }
    }

    public float MinSpawnIntervalSeconds
    {
        get { return minSpawnIntervalSeconds; }
    }

    public float MaxSpawnIntervalSeconds
    {
        get { return maxSpawnIntervalSeconds; }
    }

    public int StandardBlockPoints
    {
        get { return standardBlockPoints; }
    }

    public ConfigurationData()
    {
        // Not reading from the CSV since it doesn't work in WebGL builds.

        //try
        //{
        //    var configurationTextLines = File.ReadAllLines(Path.Combine(Application.streamingAssetsPath, ConfigurationDataFileName));
        //    var configurationStrings = configurationTextLines[1].Split(',');

        //    paddleMoveUnitsPerSecond = float.Parse(configurationStrings[0]);
        //    ballImpulseForce = float.Parse(configurationStrings[1]);
        //    ballLifespanSeconds = float.Parse(configurationStrings[2]);

        //    minSpawnIntervalSeconds = float.Parse(configurationStrings[3]);
        //    maxSpawnIntervalSeconds = float.Parse(configurationStrings[4]);

        //    standardBlockPoints = int.Parse(configurationStrings[5]);
        //}
        //catch (Exception)
        //{
        //}
    }
}
