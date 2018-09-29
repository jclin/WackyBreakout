using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    private static readonly List<UnityAction<float>> FreezerEffectActivatedListeners;
    private static readonly List<PickupBlock> FreezerEffectActivatedInvokers;

    private static readonly List<UnityAction<float, float>> SpeedupEffectActivatedListeners;
    private static readonly List<PickupBlock> SpeedupEffectActivatedInvokers;

    private static readonly List<UnityAction<int, int>> LevelBuiltListeners;
    private static readonly List<LevelBuilder> LevelBuiltInvokers;

    private static readonly List<UnityAction> BallLostListeners;
    private static readonly List<Ball> BallLostInvokers;

    private static readonly List<UnityAction<int>> BlockDestroyedListeners;
    private static readonly List<Block> BlockDestroyedInvokers;

    private static readonly List<UnityAction> GameEndedListeners;
    private static readonly List<HUD> GameEndedInvokers;

    private static readonly List<UnityAction<ComponentType>> ComponentReadyListeners;
    private static readonly List<IGameComponent> ComponentReadyInvokers;

    private static readonly List<UnityAction> GameReadyListeners;
    private static readonly List<GameInitializer> GameReadyInvokers;

    static EventManager()
    {
        FreezerEffectActivatedListeners = new List<UnityAction<float>>();
        FreezerEffectActivatedInvokers = new List<PickupBlock>();

        SpeedupEffectActivatedListeners = new List<UnityAction<float, float>>();
        SpeedupEffectActivatedInvokers = new List<PickupBlock>();

        LevelBuiltListeners = new List<UnityAction<int, int>>();
        LevelBuiltInvokers = new List<LevelBuilder>();

        BallLostListeners = new List<UnityAction>();
        BallLostInvokers = new List<Ball>();

        BlockDestroyedListeners = new List<UnityAction<int>>();
        BlockDestroyedInvokers = new List<Block>();

        GameEndedListeners = new List<UnityAction>();
        GameEndedInvokers = new List<HUD>();

        ComponentReadyListeners = new List<UnityAction<ComponentType>>();
        ComponentReadyInvokers = new List<IGameComponent>();

        GameReadyListeners = new List<UnityAction>();
        GameReadyInvokers = new List<GameInitializer>();
    }

    public static void AddFreezerEffectActivatedInvoker(PickupBlock invoker)
    {
        FreezerEffectActivatedInvokers.Add(invoker);
        foreach (var freezerEffectActivatedListener in FreezerEffectActivatedListeners)
        {
            invoker.AddFreezerEffectListener(freezerEffectActivatedListener);
        }
    }

    public static void AddFreezerEffectActivatedListener(UnityAction<float> listener)
    {
        FreezerEffectActivatedListeners.Add(listener);
        foreach (var freezerEffectActivatedInvoker in FreezerEffectActivatedInvokers)
        {
            freezerEffectActivatedInvoker.AddFreezerEffectListener(listener);
        }
    }

    public static void AddSpeedupEffectActivatedInvoker(PickupBlock invoker)
    {
        SpeedupEffectActivatedInvokers.Add(invoker);
        foreach (var speedupEffectActivatedListener in SpeedupEffectActivatedListeners)
        {
            invoker.AddSpeedupEffectListener(speedupEffectActivatedListener);
        }
    }

    public static void AddSpeedupEffectActivatedListener(UnityAction<float, float> listener)
    {
        SpeedupEffectActivatedListeners.Add(listener);
        foreach (var speedupEffectActivatedInvoker in SpeedupEffectActivatedInvokers)
        {
            speedupEffectActivatedInvoker.AddSpeedupEffectListener(listener);
        }
    }

    public static void AddLevelBuiltInvoker(LevelBuilder invoker)
    {
        LevelBuiltInvokers.Add(invoker);
        foreach (var levelBuiltListener in LevelBuiltListeners)
        {
            invoker.AddLevelBuiltListener(levelBuiltListener);
        }
    }

    public static void AddLevelBuiltListener(UnityAction<int, int> listener)
    {
        LevelBuiltListeners.Add(listener);
        foreach (var levelBuiltInvoker in LevelBuiltInvokers)
        {
            levelBuiltInvoker.AddLevelBuiltListener(listener);
        }
    }

    public static void AddBallLostInvoker(Ball invoker)
    {
        BallLostInvokers.Add(invoker);
        foreach (var ballLostListener in BallLostListeners)
        {
            invoker.AddBallLostListener(ballLostListener);
        }
    }

    public static void AddBallLostListener(UnityAction listener)
    {
        BallLostListeners.Add(listener);
        foreach (var ballLostInvoker in BallLostInvokers)
        {
            ballLostInvoker.AddBallLostListener(listener);
        }
    }

    public static void AddBlockDestroyedInvoker(Block invoker)
    {
        BlockDestroyedInvokers.Add(invoker);
        foreach (var listener in BlockDestroyedListeners)
        {
            invoker.AddBlockDestroyedListener(listener);
        }
    }

    public static void AddBlockDestroyedListener(UnityAction<int> listener)
    {
        BlockDestroyedListeners.Add(listener);
        foreach (var invoker in BlockDestroyedInvokers)
        {
            invoker.AddBlockDestroyedListener(listener);
        }
    }

    public static void AddGameEndedInvoker(HUD invoker)
    {
        GameEndedInvokers.Add(invoker);
        foreach (var listener in GameEndedListeners)
        {
            invoker.AddGameEndedListener(listener);
        }
    }

    public static void AddGameEndedListener(UnityAction listener)
    {
        GameEndedListeners.Add(listener);
        foreach (var invoker in GameEndedInvokers)
        {
            invoker.AddGameEndedListener(listener);
        }
    }

    public static void AddComponentReadyInvoker(IGameComponent invoker)
    {
        ComponentReadyInvokers.Add(invoker);
        foreach (var listener in ComponentReadyListeners)
        {
            invoker.AddComponentReadyListener(listener);
        }
    }

    public static void AddComponentReadyListener(UnityAction<ComponentType> listener)
    {
        ComponentReadyListeners.Add(listener);
        foreach (var invoker in ComponentReadyInvokers)
        {
            invoker.AddComponentReadyListener(listener);
        }
    }

    public static void AddGameReadyInvoker(GameInitializer invoker)
    {
        GameReadyInvokers.Add(invoker);
        foreach (var listener in GameReadyListeners)
        {
            invoker.AddGameReadyListener(listener);
        }
    }

    public static void AddGameReadyListener(UnityAction listener)
    {
        GameReadyListeners.Add(listener);
        foreach (var invoker in GameReadyInvokers)
        {
            invoker.AddGameReadyListener(listener);
        }
    }
}