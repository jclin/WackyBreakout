using UnityEngine;
using UnityEngine.Events;

public abstract class GameComponent<T> : MonoBehaviour, IGameComponent where T : GameComponent<T>
{
    public abstract ComponentType ComponentType
    {
        get;
    }

    private ComponentReady componentReadyEvent;

    public void AddComponentReadyListener(UnityAction<ComponentType> listener)
    {
        componentReadyEvent.AddListener(listener);
    }

    protected abstract void Initialize();
    protected abstract void GameStarting();

    private void Awake()
    {
        componentReadyEvent = new ComponentReady();
        EventManager.AddComponentReadyInvoker(this);

        EventManager.AddGameReadyListener(OnGameReady);

        Initialize();

        GetComponent<T>().enabled = false;
        componentReadyEvent.Invoke(ComponentType);
    }

    private void OnGameReady()
    {
        GameStarting();
        GetComponent<T>().enabled = true;
    }
}
