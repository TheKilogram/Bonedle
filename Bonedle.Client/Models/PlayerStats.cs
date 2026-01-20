namespace Bonedle.Client.Models;

public class PlayerStats
{
    public int GamesPlayed { get; set; }
    public int GamesWon { get; set; }
    public int CurrentStreak { get; set; }
    public int MaxStreak { get; set; }
    public int[] GuessDistribution { get; set; } = new int[10]; // 1-10 guesses
    public string LastPlayedDate { get; set; } = string.Empty;
}
