
public class DJEvent : Unity.Services.Analytics.Event
{
    public DJEvent(string name, bool gameWon, int score) : base(name)
    {
        GameWon = gameWon;
        Score = score;
    }

    public bool GameWon
    {
        set => SetParameter("gameWon", value);
    }

    public int Score
    {
        set => SetParameter("score", value);
    }
}
