using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HUD : GameComponent<HUD>
{
    private const string BallsTag = "BallsLeft";
    private const string BallsLeftFormat = "{0:D2} balls left";

    private const string ScoreTag = "Score";
    private const string ScoreFormat = "{0:D4} points";

    public override ComponentType ComponentType
    {
        get
        {
            return ComponentType.HUD;
        }
    }

    public Text ballsLeftText;

    public Text scoreText;

    private int ballsLeft;
    private int blocksLeft;

    private GameEnded gameEndedEvent;

    protected override void Initialize()
    {
        gameEndedEvent = new GameEnded();

        var ballsLeftGameObject = GameObject.FindGameObjectWithTag(BallsTag);
        ballsLeftText = ballsLeftGameObject.GetComponent<Text>();

        var scoreGameObject = GameObject.FindGameObjectWithTag(ScoreTag);
        scoreText = scoreGameObject.GetComponent<Text>();

        EventManager.AddGameEndedInvoker(this);
        EventManager.AddBallLostListener(OnBallLost);
        EventManager.AddBlockDestroyedListener(OnBlockDestroyed);

        GetComponent<HUD>().enabled = false;
    }

    protected override void GameStarting()
    {
        ballsLeft = LevelState.TotalBalls;
        UpdateBallsLeftText();

        blocksLeft = LevelState.TotalBlocks;
    }

    public void AddGameEndedListener(UnityAction listener)
    {
        gameEndedEvent.AddListener(listener);
    }

    private void OnBallLost()
    {
        ballsLeft--;
        UpdateBallsLeftText();

        if (ballsLeft > 0)
        {
            return;
        }

        EndGame(gameWon: false);
    }

    private void OnBlockDestroyed(int points)
    {
        blocksLeft--;

        LevelState.IncrementScore((byte)points);
        UpdateScoreText();

        if (blocksLeft > 0)
        {
            return;
        }

        EndGame(gameWon: true);
    }

    private void UpdateBallsLeftText()
    {
        ballsLeftText.text = string.Format(BallsLeftFormat, ballsLeft);
    }

    private void UpdateScoreText()
    {
        scoreText.text = string.Format(ScoreFormat, LevelState.Score);
    }

    private void EndGame(bool gameWon)
    {
        LevelState.End(gameWon);

        gameEndedEvent.Invoke();
        MenuManager.GoToMenu(MenuName.GameFinished);
    }
}