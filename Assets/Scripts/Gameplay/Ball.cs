using UnityEngine;
using UnityEngine.Events;

public class Ball : MonoBehaviour
{
    private const string BallTag = "Ball";

    private const float InitialAngleDegrees = -90;
    private const float StartMovementWaitTimeSeconds = 1f;

    private bool ShouldSpeedUp
    {
        get { return EffectUtils.SpeedupActive && EffectUtils.SecondsRemaining > 0f && !spedUp; }
    }

    private bool ShouldSlowDown
    {
        get { return spedUp && EffectUtils.ExitedSpeedup; }
    }

    private Timer startMovementTimer;
    private Timer lifespanTimer;

    private bool gameEnded;

    private bool spedUp;
    private bool movementStarted;

    private BallLost ballLostEvent;

    private void Awake()
    {
        ballLostEvent = new BallLost();
        EventManager.AddBallLostInvoker(this);
        EventManager.AddGameEndedListener(OnGameEnded);
    }

    private void Start()
    {
        startMovementTimer = GetComponents<Timer>()[0];
        startMovementTimer.Duration = StartMovementWaitTimeSeconds;
        startMovementTimer.Run();

        lifespanTimer = GetComponents<Timer>()[1];
        lifespanTimer.Duration = ConfigurationUtils.BallLifespanSeconds;
    }

    public void SetDirection(Vector2 direction)
    {
        var rigidBody = GetComponent<Rigidbody2D>();
        var speed = rigidBody.velocity.magnitude;
        rigidBody.velocity = speed * direction;
    }

    public void AddBallLostListener(UnityAction listener)
    {
        ballLostEvent.AddListener(listener);
    }

    private void Update()
    {
        if (!startMovementTimer.Finished)
        {
            return;
        }

        if (!movementStarted)
        {
            StartMovement();
        }
        else
        {
            AdjustSpeed();
        }

        if (!lifespanTimer.Finished)
        {
            return;
        }

        SpawnNewInstance();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag(BallTag))
        {
            return;
        }

        AudioManager.Play(AudioClipName.BallCollidedWithBall);
    }

    private void OnBecameInvisible()
    {
        // only spawn a new ball if below screen
        var halfColliderHeight = gameObject.GetComponent<BoxCollider2D>().size.y / 2;
        if (transform.position.y - halfColliderHeight < ScreenUtils.ScreenBottom && !lifespanTimer.Finished)
        {
            AudioManager.Play(AudioClipName.BallLost);

            ballLostEvent.Invoke();
            SpawnNewInstance();
        }
    }

    private void OnGameEnded()
    {
        gameEnded = true;
    }

    private void StartMovement()
    {
        movementStarted = true;

        var initialAngleRads = InitialAngleDegrees * Mathf.Deg2Rad;

        GetComponent<Rigidbody2D>().AddForce(ConfigurationUtils.BallImpulseForce * new Vector2(Mathf.Cos(initialAngleRads), Mathf.Sin(initialAngleRads)), ForceMode2D.Impulse);

        lifespanTimer.Run();
    }

    private void AdjustSpeed()
    {
        if (ShouldSlowDown)
        {
            SlowDown();
            return;
        }

        if (ShouldSpeedUp)
        {
            SpeedUp();
        }
    }

    private void SpawnNewInstance()
    {
        Destroy(gameObject);

        if (gameEnded)
        {
            return;
        }

        if (Camera.main == null)
        {
            return;
        }

        var ballSpawner = Camera.main.GetComponent<BallSpawner>();
        if (ballSpawner != null)
        {
            ballSpawner.TrySpawn();
        }
    }

    private void SpeedUp()
    {
        GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity * EffectUtils.SpeedupFactor;
        spedUp = true;
    }

    private void SlowDown()
    {
        GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity / EffectUtils.SpeedupFactor;
        spedUp = false;
    }
}