public class Game
{
    public int ScorePlayer1 { get; set; }
    public int Level { get; set; }
    public bool IsGameOver { get; set; }

    public Game()
    {
        ScorePlayer1 = 0;
        Level = 1;
        IsGameOver = false;
    }

    public void Start()
    {
        // Initialize game
    }

    public void Pause()
    {
        // Pause game logic
    }

    public void End()
    {
        // End game logic
    }
}

