using UnityEngine;
using UnityEngine.UI;

public class GameFinishedMenu : Menu
{
    private const string FinalScoreTextTag = "FinalScoreText";

    protected override void Initialize()
    {
        var finalScoreText = GameObject.FindGameObjectWithTag(FinalScoreTextTag).GetComponent<Text>();
        finalScoreText.text = string.Format("Game Finished. Your final score is {0:D4} points.", LevelState.Score);

        if (LevelState.GameWon.HasValue)
        {
            AudioManager.Play(LevelState.GameWon.Value ? AudioClipName.GameWon : AudioClipName.GameLost);
        }
    }

    public void HandleQuitOnClickEvent()
    {
        GoToMenu(MenuName.Main);
    }
}