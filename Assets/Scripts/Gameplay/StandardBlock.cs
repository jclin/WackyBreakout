using System;
using UnityEngine;
using Random = UnityEngine.Random;

public sealed class StandardBlock : Block
{
    [SerializeField]
    private Sprite yellowSprite;

    [SerializeField]
    private Sprite greenSprite;

    [SerializeField]
    private Sprite redSprite;

    protected override void Start()
    {
        base.Start();

        InitializeSprite();
    }

    protected override int GetPoints()
    {
        return ConfigurationUtils.StandardBlockPoints;
    }

    private void InitializeSprite()
    {
        var spriteIndex = Random.Range(1, 4);
        switch (spriteIndex)
        {
            case 1:
                GetComponent<SpriteRenderer>().sprite = yellowSprite;
                break;

            case 2:
                GetComponent<SpriteRenderer>().sprite = greenSprite;
                break;

            case 3:
                GetComponent<SpriteRenderer>().sprite = redSprite;
                break;

            default:
                throw new InvalidOperationException();
        }
    }
}
