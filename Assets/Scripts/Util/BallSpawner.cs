using UnityEngine;

public sealed class BallSpawner : GameComponent<BallSpawner>
{
    [SerializeField]
    private GameObject ballPrefab;

    public override ComponentType ComponentType
    {
        get
        {
            return ComponentType.BallSpawner;
        }
    }

    private bool retrySpawn;

    private Vector2 spawnPosBottomLeft;
    private Vector2 spawnPosTopRight;
    private Timer spawnTimer;

    protected override void Initialize()
    {
        InitializeSpawnPosition();
        spawnTimer = gameObject.AddComponent<Timer>();
    }

    protected override void GameStarting()
    {
        SpawnBall();
        StartSpawnTimer();
    }

    private void Update()
    {
        bool? spawned = null;
        if (retrySpawn)
        {
            spawned = TrySpawn();
            if (spawnTimer.Finished)
            {
                StartSpawnTimer();
            }
        }
        else if (spawnTimer.Finished)
        {
            spawned = TrySpawn();
            if (spawned.Value)
            {
                StartSpawnTimer();
            }
        }

        if (spawned.HasValue)
        {
            retrySpawn = !spawned.Value;
        }
    }

    public bool TrySpawn()
    {
        if (Physics2D.OverlapArea(spawnPosBottomLeft, spawnPosTopRight) != null)
        {
            return false;
        }

        SpawnBall();

        return true;
    }

    private void StartSpawnTimer()
    {
        spawnTimer.Duration = Random.Range(ConfigurationUtils.MinSpawnIntervalSeconds, ConfigurationUtils.MaxSpawnIntervalSeconds);
        spawnTimer.Run();
    }

    private void InitializeSpawnPosition()
    {
        var tempBall = Instantiate(ballPrefab);

        var boxCollider = tempBall.GetComponent<BoxCollider2D>();
        spawnPosBottomLeft = new Vector2(tempBall.transform.position.x - boxCollider.size.x / 2f, tempBall.transform.position.y - boxCollider.size.y / 2);
        spawnPosTopRight = new Vector2(tempBall.transform.position.x + boxCollider.size.x / 2f, tempBall.transform.position.y + boxCollider.size.y / 2);

        Destroy(tempBall);
    }

    private void SpawnBall()
    {
        Instantiate(ballPrefab);
        AudioManager.Play(AudioClipName.BallSpawned);
    }
}