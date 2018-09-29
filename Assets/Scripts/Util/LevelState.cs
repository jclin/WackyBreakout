public static class LevelState
{
    public static int TotalBalls
    {
        get;
        private set;
    }

    public static int TotalBlocks
    {
        get;
        private set;
    }

    public static int Score
    {
        get;
        private set;
    }

    public static bool? GameWon
    {
        get;
        private set;
    }

    public static void Reset(int totalBalls, int totalBlocks)
    {
        TotalBalls = totalBalls;
        TotalBlocks = totalBlocks;
        Score = 0;
        GameWon = null;
    }

    public static void IncrementScore(byte points)
    {
        Score += points;
    }

    public static void End(bool gameWon)
    {
        GameWon = gameWon;
    }
}
