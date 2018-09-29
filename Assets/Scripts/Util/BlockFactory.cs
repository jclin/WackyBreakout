using System;
using UnityEngine;

public sealed class BlockFactory
{
    public float Probability { get; private set; }
    public Func<GameObject> PrefabFunc { get; private set; }

    public BlockFactory(float probability, Func<GameObject> prefabFunc)
    {
        Probability = probability;
        PrefabFunc = prefabFunc;
    }
}
