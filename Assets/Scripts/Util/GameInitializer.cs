using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public sealed class GameInitializer : MonoBehaviour
{
    private List<ComponentType> gameComponents;
    private GameReady gameReadyEvent;

    private void Awake()
    {
        gameComponents = new List<ComponentType> { ComponentType.BallSpawner, ComponentType.HUD, ComponentType.LevelBuilder };
        gameReadyEvent = new GameReady();

        ConfigurationUtils.Initialize();
        ConfigurationUtils.ConfigurationData = new ConfigurationData();
        ScreenUtils.Initialize();

        EventManager.AddComponentReadyListener(OnComponentReady);
        EventManager.AddGameReadyInvoker(this);
    }

    public void AddGameReadyListener(UnityAction listener)
    {
        gameReadyEvent.AddListener(listener);
    }

    private void OnComponentReady(ComponentType componentType)
    {
        if (gameComponents.Contains(componentType))
        {
            gameComponents.Remove(componentType);
        }

        if (gameComponents.Count > 0)
        {
            return;
        }

        gameReadyEvent.Invoke();
    }
}
