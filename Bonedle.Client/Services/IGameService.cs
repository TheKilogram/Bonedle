using Bonedle.Client.Models;

namespace Bonedle.Client.Services;

public interface IGameService
{
    Task<GameState> GetOrCreateGameAsync(GameMode mode, DifficultyLevel difficulty, string? date = null);
    Task<GuessResult> MakeGuessAsync(GameState state, string boneNameOrId);
    Task<PlayerStats> GetStatsAsync(GameMode mode, DifficultyLevel difficulty);
    Task<bool> CanPlayFullModeAsync();
    Task UnlockFullModeAsync();
    int MaxGuesses { get; }
    List<string> GetHints(GameState state);
    List<string> GetAllHints(GameState state);
    NameGuessResult CreateNameGuessResult(string guessedBoneId, string targetBoneId);
}
