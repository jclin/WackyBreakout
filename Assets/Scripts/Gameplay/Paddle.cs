using UnityEngine;

public sealed class Paddle : MonoBehaviour
{
    private const string HorizontalInputAxisName = "Horizontal";
    private const float BounceAngleHalfRange = 60 * Mathf.Deg2Rad;

    private bool isFrozen;
    private Timer freezeTimer;

    private Rigidbody2D rigidBody;
    private float halfWidth;

    private void Start()
    {
        rigidBody  = GetComponent<Rigidbody2D>();
        halfWidth  = GetComponent<BoxCollider2D>().size.x / 2f;

        freezeTimer = gameObject.AddComponent<Timer>();

        EventManager.AddFreezerEffectActivatedListener(OnFreezerEffectActivated);
    }

    private void Update()
    {
        if (freezeTimer.Finished && freezeTimer.Started)
        {
            AudioManager.Play(AudioClipName.FreezerEffectDeactivated);
            freezeTimer.Stop();
            isFrozen = false;
        }
    }

    private void FixedUpdate()
    {
        if (isFrozen)
        {
            return;
        }

        var horizontalMovement = Input.GetAxis(HorizontalInputAxisName);
        if (Mathf.Abs(horizontalMovement) <= float.Epsilon)
        {
            return;
        }

        var moveAmount = horizontalMovement * Time.deltaTime * ConfigurationUtils.PaddleMoveUnitsPerSecond;

        rigidBody.MovePosition(new Vector2(CoerceHorizontalCenter(transform.position.x + moveAmount), transform.position.y));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Ball"))
        {
            return;
        }

        AudioManager.Play(AudioClipName.BallCollidedWithPaddle);

        if (!CollidedWithTopEdge(collision))
        {
            return;
        }

        // calculate new ball direction
        float ballOffsetFromPaddleCenter = transform.position.x - collision.transform.position.x;
        float normalizedBallOffset = ballOffsetFromPaddleCenter / halfWidth;
        float angleOffset = normalizedBallOffset * BounceAngleHalfRange;
        float angle = Mathf.PI / 2 + angleOffset;
        Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

        Ball ballScript = collision.gameObject.GetComponent<Ball>();
        ballScript.SetDirection(direction);
    }

    private void OnFreezerEffectActivated(float durationSeconds)
    {
        AudioManager.Play(AudioClipName.FreezerEffectActivated);

        if (freezeTimer.Running)
        {
            freezeTimer.Add(durationSeconds);
        }
        else
        {
            freezeTimer.Duration = durationSeconds;
            freezeTimer.Run();
        }

        isFrozen = true;
    }

    private bool CollidedWithTopEdge(Collision2D collision)
    {
        // on top collisions, both contact points are at the same y location
        var contactPoints = new ContactPoint2D[2];
        collision.GetContacts(contactPoints);

        return Mathf.Abs(contactPoints[0].point.y - contactPoints[1].point.y) < 0.05f;
    }

    private float CoerceHorizontalCenter(float centerX)
    {
        if (centerX - halfWidth < ScreenUtils.ScreenLeft)
        {
            return ScreenUtils.ScreenLeft + halfWidth;
        }

        if (centerX + halfWidth > ScreenUtils.ScreenRight)
        {
            return ScreenUtils.ScreenRight - halfWidth;
        }

        return centerX;
    }
}