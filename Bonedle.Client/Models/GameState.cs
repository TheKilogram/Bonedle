namespace Bonedle.Client.Models;

public class GameState
{
    public string Date { get; set; } = string.Empty;
    public GameMode Mode { get; set; }
    public DifficultyLevel Difficulty { get; set; }
    public List<string> Guesses { get; set; } = new();
    public bool Completed { get; set; }
    public bool Won { get; set; }
    public string TargetBoneId { get; set; } = string.Empty;
}
