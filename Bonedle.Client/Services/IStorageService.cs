using Bonedle.Client.Models;

namespace Bonedle.Client.Services;

public interface IStorageService
{
    Task<T?> GetAsync<T>(string key);
    Task SetAsync<T>(string key, T value);
    Task RemoveAsync(string key);
    Task<GameState?> GetGameStateAsync(GameMode mode, DifficultyLevel difficulty, string date);
    Task SaveGameStateAsync(GameState state);
    Task<PlayerStats> GetStatsAsync(GameMode mode, DifficultyLevel difficulty);
    Task SaveStatsAsync(GameMode mode, DifficultyLevel difficulty, PlayerStats stats);
    Task<bool> IsFullModeUnlockedAsync();
    Task UnlockFullModeAsync();
}
