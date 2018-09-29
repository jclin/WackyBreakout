using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public sealed class LevelBuilder : GameComponent<LevelBuilder>
{
    [SerializeField]
    private GameObject paddlePrefab;

    [SerializeField]
    private GameObject standardBlockPrefab;

    [SerializeField]
    private GameObject bonusBlockPrefab;

    [SerializeField]
    private GameObject pickupBlockPrefab;

    public override ComponentType ComponentType
    {
        get
        {
            return ComponentType.LevelBuilder;
        }
    }

    private Vector2 blockSize;

    private LevelBuilt levelBuiltEvent;

    private float[] blockProbabilities =
    {
        0.75f, // Standard block
        0.10f, // Freezer block
        0.10f, // Speedup block
        0.05f  // Bonus block
    };

    protected override void Initialize()
    {
        Instantiate(paddlePrefab);

        InitializeBlockSize();
        LevelState.Reset(ConfigurationUtils.TotalBalls, AddBlocks(3));
    }

    protected override void GameStarting()
    {
    }

    public void AddLevelBuiltListener(UnityAction<int, int> listener)
    {
        levelBuiltEvent.AddListener(listener);
    }

    private void InitializeBlockSize()
    {
        var tempBlock = Instantiate(standardBlockPrefab);
        blockSize = tempBlock.GetComponent<BoxCollider2D>().size;

        Destroy(tempBlock);
    }

    private int AddBlocks(int rows)
    {
        int blocksCreated = 0;

        var rowTop = ScreenUtils.ScreenTop - (ScreenUtils.ScreenTop - ScreenUtils.ScreenBottom) / 5f - (blockSize.y / 2f);
        for (var i = 0; i < rows; i++)
        {
            blocksCreated += AddBlocksRow(rowTop);
            rowTop += blockSize.y;
        }

        return blocksCreated;
    }

    private int AddBlocksRow(float rowTop)
    {
        var blocksPerRow = Mathf.CeilToInt(ScreenUtils.ScreenWidth / blockSize.x);

        var currLeftEdgeX = ScreenUtils.ScreenLeft - 0.5f * (blocksPerRow * blockSize.x - ScreenUtils.ScreenWidth);
        for (var i = 0; i < blocksPerRow; i++)
        {
            var block = SpawnBlock(ChooseBlockPrefabIndex(blockProbabilities));
            block.transform.position = new Vector3(currLeftEdgeX + blockSize.x / 2f, rowTop + blockSize.y / 2f, block.transform.position.z);

            currLeftEdgeX += blockSize.x;
        }

        return blocksPerRow;
    }

    private GameObject SpawnBlock(int blockIndex)
    {
        switch (blockIndex)
        {
            case 0: // Standard
                return Instantiate(standardBlockPrefab);

            case 1: // Freeze
                var freezerBlock = Instantiate(pickupBlockPrefab);
                freezerBlock.GetComponent<PickupBlock>().PickupEffect = PickupEffect.Freezer;
                return freezerBlock;

            case 2: // Speedup
                var speedupBlock = Instantiate(pickupBlockPrefab);
                speedupBlock.GetComponent<PickupBlock>().PickupEffect = PickupEffect.Speedup;
                return speedupBlock;

            case 3:
                return Instantiate(bonusBlockPrefab);

            default:
                throw new InvalidOperationException();
        }
    }

    private static int ChooseBlockPrefabIndex(float[] blockProbabilities)
    {
        var total = blockProbabilities.Sum();
        var randomPoint = Random.value * total;

        for (var i= 0; i < blockProbabilities.Length; i++)
        {
            if (randomPoint < blockProbabilities[i])
            {
                return i;
            }

            randomPoint -= blockProbabilities[i];
        }

        return blockProbabilities.Length - 1;
    }
}