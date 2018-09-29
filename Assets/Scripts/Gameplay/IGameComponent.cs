using UnityEngine.Events;

public interface IGameComponent
{
    ComponentType ComponentType
    {
        get;
    }

    void AddComponentReadyListener(UnityAction<ComponentType> listener);
}
