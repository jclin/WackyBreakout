
using UnityEngine;

public sealed class BonusBlock : Block
{
    [SerializeField]
    private Sprite orangeSprite;

    private const int BonusPoints = 3;

    protected override void Start()
    {
        base.Start();

        GetComponent<SpriteRenderer>().sprite = orangeSprite;
    }

    protected override int GetPoints()
    {
        return BonusPoints;
    }
}
