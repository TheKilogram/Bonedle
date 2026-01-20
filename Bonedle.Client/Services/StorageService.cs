using System.Text.Json;
using Bonedle.Client.Models;
using Microsoft.JSInterop;

namespace Bonedle.Client.Services;

public class StorageService : IStorageService
{
    private readonly IJSRuntime _jsRuntime;
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public StorageService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        var json = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", key);
        if (string.IsNullOrEmpty(json))
            return default;

        return JsonSerializer.Deserialize<T>(json, _jsonOptions);
    }

    public async Task SetAsync<T>(string key, T value)
    {
        var json = JsonSerializer.Serialize(value, _jsonOptions);
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, json);
    }

    public async Task RemoveAsync(string key)
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
    }

    private static string GetGameStateKey(GameMode mode, DifficultyLevel difficulty, string date)
    {
        return $"bonedle_game_{mode.ToString().ToLower()}_{difficulty.ToString().ToLower()}_{date}";
    }

    private static string GetStatsKey(GameMode mode, DifficultyLevel difficulty)
    {
        return $"bonedle_stats_{mode.ToString().ToLower()}_{difficulty.ToString().ToLower()}";
    }

    public async Task<GameState?> GetGameStateAsync(GameMode mode, DifficultyLevel difficulty, string date)
    {
        var key = GetGameStateKey(mode, difficulty, date);
        return await GetAsync<GameState>(key);
    }

    public async Task SaveGameStateAsync(GameState state)
    {
        var key = GetGameStateKey(state.Mode, state.Difficulty, state.Date);
        await SetAsync(key, state);
    }

    public async Task<PlayerStats> GetStatsAsync(GameMode mode, DifficultyLevel difficulty)
    {
        var key = GetStatsKey(mode, difficulty);
        return await GetAsync<PlayerStats>(key) ?? new PlayerStats();
    }

    public async Task SaveStatsAsync(GameMode mode, DifficultyLevel difficulty, PlayerStats stats)
    {
        var key = GetStatsKey(mode, difficulty);
        await SetAsync(key, stats);
    }

    public async Task<bool> IsFullModeUnlockedAsync()
    {
        return await GetAsync<bool>("bonedle_unlocked_full");
    }

    public async Task UnlockFullModeAsync()
    {
        await SetAsync("bonedle_unlocked_full", true);
    }
}
