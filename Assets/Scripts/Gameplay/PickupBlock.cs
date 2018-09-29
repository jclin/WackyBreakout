using System;
using UnityEngine;
using UnityEngine.Events;

public sealed class PickupBlock : Block
{
    [SerializeField]
    private Sprite freezerBlockSprite;

    [SerializeField]
    private Sprite speedupBlockSprite;

    private const int PickupPoints = 2;

    public PickupEffect PickupEffect
    {
        get
        {
            return pickupEffect;
        }
        set
        {
            pickupEffect = value;
            GetComponent<SpriteRenderer>().sprite = pickupEffect == PickupEffect.Freezer ? freezerBlockSprite : speedupBlockSprite;
        }
    }

    public int EffectDurationSeconds
    {
        get
        {
            return PickupEffect == PickupEffect.Freezer ? ConfigurationUtils.FreezerDurationSeconds : ConfigurationUtils.SpeedupDurationSeconds;
        }
    }

    private PickupEffect pickupEffect;

    private readonly FreezerEffectActivated freezerEffectActivated;
    private readonly SpeedupEffectActivated speedupEffectActivated;

    public PickupBlock()
    {
        freezerEffectActivated = new FreezerEffectActivated();
        speedupEffectActivated = new SpeedupEffectActivated();

        EventManager.AddFreezerEffectActivatedInvoker(this);
        EventManager.AddSpeedupEffectActivatedInvoker(this);
    }

    public void AddFreezerEffectListener(UnityAction<float> listener)
    {
        freezerEffectActivated.AddListener(listener);
    }

    public void AddSpeedupEffectListener(UnityAction<float, float> listener)
    {
        speedupEffectActivated.AddListener(listener);
    }

    protected override int GetPoints()
    {
        return PickupPoints;
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        switch (PickupEffect)
        {
            case PickupEffect.Freezer:
                freezerEffectActivated.Invoke(ConfigurationUtils.FreezerDurationSeconds);
                break;

            case PickupEffect.Speedup:
                speedupEffectActivated.Invoke(ConfigurationUtils.SpeedupDurationSeconds, ConfigurationUtils.SpeedupFactor);
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }

        base.OnCollisionEnter2D(collision);
    }
}

