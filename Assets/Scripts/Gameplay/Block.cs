using UnityEngine;
using UnityEngine.Events;

public abstract class Block : MonoBehaviour
{
    protected int Points
    {
        get;
        private set;
    }

    private readonly BlockDestroyed blockDestroyedEvent;

    protected Block()
    {
        blockDestroyedEvent = new BlockDestroyed();
        EventManager.AddBlockDestroyedInvoker(this);
    }

    public void AddBlockDestroyedListener(UnityAction<int> listener)
    {
        blockDestroyedEvent.AddListener(listener);
    }

    protected virtual void Start()
    {
        Points = GetPoints();
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        AudioManager.Play(AudioClipName.BallCollidedWithBlock);

        blockDestroyedEvent.Invoke(Points);
        Destroy(gameObject);
    }

    protected abstract int GetPoints();
}