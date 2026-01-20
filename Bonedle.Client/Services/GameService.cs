using Bonedle.Client.Models;

namespace Bonedle.Client.Services;

public class GameService : IGameService
{
    private readonly IStorageService _storageService;
    private readonly IBoneDataService _boneDataService;
    private readonly IDailyPuzzleService _dailyPuzzleService;

    public int MaxGuesses => 10;

    public GameService(
        IStorageService storageService,
        IBoneDataService boneDataService,
        IDailyPuzzleService dailyPuzzleService)
    {
        _storageService = storageService;
        _boneDataService = boneDataService;
        _dailyPuzzleService = dailyPuzzleService;
    }

    public async Task<GameState> GetOrCreateGameAsync(GameMode mode, DifficultyLevel difficulty, string? date = null)
    {
        date ??= _dailyPuzzleService.GetTodaysDate();
        var existingState = await _storageService.GetGameStateAsync(mode, difficulty, date);

        if (existingState != null)
            return existingState;

        var targetBoneId = _dailyPuzzleService.GetDailyBoneId(mode, difficulty, DateTime.Parse(date));

        var newState = new GameState
        {
            Date = date,
            Mode = mode,
            Difficulty = difficulty,
            Guesses = new List<string>(),
            Completed = false,
            Won = false,
            TargetBoneId = targetBoneId
        };

        await _storageService.SaveGameStateAsync(newState);
        return newState;
    }

    public async Task<GuessResult> MakeGuessAsync(GameState state, string boneNameOrId)
    {
        if (state.Completed)
            return new GuessResult("", "", false, -1, "");

        var bone = _boneDataService.GetBoneByName(boneNameOrId) ??
                   _boneDataService.GetBoneById(boneNameOrId);

        if (bone == null)
            return new GuessResult("", boneNameOrId, false, -1, "#666666");

        // Check if already guessed
        if (state.Guesses.Contains(bone.Id))
            return new GuessResult(bone.Id, bone.DisplayName, false, -2, "#666666");

        // Add guess
        state.Guesses.Add(bone.Id);

        var isCorrect = bone.Id == state.TargetBoneId;
        var distance = isCorrect ? 0 : _boneDataService.CalculateDistance(bone.Id, state.TargetBoneId);
        var heatColor = _boneDataService.GetHeatColor(distance);

        // Check for game end
        if (isCorrect || state.Guesses.Count >= MaxGuesses)
        {
            state.Completed = true;
            state.Won = isCorrect;

            // Update stats
            await UpdateStatsAsync(state);

            // Check for full mode unlock
            if (isCorrect && state.Difficulty == DifficultyLevel.Major)
            {
                await _storageService.UnlockFullModeAsync();
            }
        }

        await _storageService.SaveGameStateAsync(state);

        return new GuessResult(bone.Id, bone.DisplayName, isCorrect, distance, heatColor);
    }

    private async Task UpdateStatsAsync(GameState state)
    {
        var stats = await _storageService.GetStatsAsync(state.Mode, state.Difficulty);

        stats.GamesPlayed++;

        if (state.Won)
        {
            stats.GamesWon++;
            var guessCount = state.Guesses.Count;
            if (guessCount >= 1 && guessCount <= 10)
            {
                stats.GuessDistribution[guessCount - 1]++;
            }

            // Update streak
            if (stats.LastPlayedDate == GetYesterdaysDate() || string.IsNullOrEmpty(stats.LastPlayedDate))
            {
                stats.CurrentStreak++;
            }
            else if (stats.LastPlayedDate != state.Date)
            {
                stats.CurrentStreak = 1;
            }

            stats.MaxStreak = Math.Max(stats.MaxStreak, stats.CurrentStreak);
        }
        else
        {
            stats.CurrentStreak = 0;
        }

        stats.LastPlayedDate = state.Date;
        await _storageService.SaveStatsAsync(state.Mode, state.Difficulty, stats);
    }

    private static string GetYesterdaysDate()
    {
        return DateTime.UtcNow.AddDays(-1).ToString("yyyy-MM-dd");
    }

    public async Task<PlayerStats> GetStatsAsync(GameMode mode, DifficultyLevel difficulty)
    {
        return await _storageService.GetStatsAsync(mode, difficulty);
    }

    public async Task<bool> CanPlayFullModeAsync()
    {
        return await _storageService.IsFullModeUnlockedAsync();
    }

    public async Task UnlockFullModeAsync()
    {
        await _storageService.UnlockFullModeAsync();
    }

    public List<string> GetHints(GameState state)
    {
        var allHints = GetAllHints(state);
        var guessCount = state.Guesses.Count;
        return allHints.Take(guessCount).ToList();
    }

    public List<string> GetAllHints(GameState state)
    {
        var hints = new List<string>();
        var targetBone = _boneDataService.GetBoneById(state.TargetBoneId);

        if (targetBone == null)
            return hints;

        // All hints available (user clicks to reveal)
        hints.Add($"Category: {(targetBone.Category == "axial" ? "Axial" : "Appendicular")}");
        hints.Add($"Region: {FormatRegion(targetBone.Region)}");
        hints.Add($"Count in body: {targetBone.CountInBody}");
        hints.Add($"First letter: {targetBone.DisplayName[0]}");

        var connectedNames = targetBone.AdjacentBones
            .Select(id => _boneDataService.GetBoneById(id)?.DisplayName)
            .Where(n => n != null)
            .Take(3)
            .ToList();
        hints.Add($"Connects to: {string.Join(", ", connectedNames)}");

        hints.Add($"Type: {FormatBoneType(targetBone.BoneType)}");
        hints.Add($"Letters in name: {targetBone.DisplayName.Replace(" ", "").Length}");

        return hints;
    }

    public NameGuessResult CreateNameGuessResult(string guessedBoneId, string targetBoneId)
    {
        var guessedBone = _boneDataService.GetBoneById(guessedBoneId);
        var targetBone = _boneDataService.GetBoneById(targetBoneId);

        if (guessedBone == null || targetBone == null)
            return null!;

        var isCorrect = guessedBoneId == targetBoneId;

        // Category match (Axial vs Appendicular)
        var categoryMatch = new CategoryMatch(
            "Category",
            guessedBone.Category == "axial" ? "Axial" : "Appendicular",
            targetBone.Category == "axial" ? "Axial" : "Appendicular",
            guessedBone.Category == targetBone.Category ? MatchStatus.Correct : MatchStatus.Wrong
        );

        // Region match
        var regionMatch = new CategoryMatch(
            "Region",
            FormatRegion(guessedBone.Region),
            FormatRegion(targetBone.Region),
            GetRegionMatchStatus(guessedBone.Region, targetBone.Region)
        );

        // Bone type match
        var boneTypeMatch = new CategoryMatch(
            "Type",
            FormatBoneType(guessedBone.BoneType).Replace(" bone", ""),
            FormatBoneType(targetBone.BoneType).Replace(" bone", ""),
            guessedBone.BoneType == targetBone.BoneType ? MatchStatus.Correct : MatchStatus.Wrong
        );

        // Count match
        var countMatch = new CategoryMatch(
            "Count",
            guessedBone.CountInBody.ToString(),
            targetBone.CountInBody.ToString(),
            GetCountMatchStatus(guessedBone.CountInBody, targetBone.CountInBody)
        );

        return new NameGuessResult(
            guessedBoneId,
            guessedBone.DisplayName,
            isCorrect,
            categoryMatch,
            regionMatch,
            boneTypeMatch,
            countMatch
        );
    }

    private static MatchStatus GetRegionMatchStatus(string guessRegion, string targetRegion)
    {
        if (guessRegion == targetRegion)
            return MatchStatus.Correct;

        // Define region groups for partial matches
        var upperBody = new[] { "head", "spine", "torso" };
        var arms = new[] { "arm", "hand" };
        var lowerBody = new[] { "pelvis", "leg", "foot" };

        bool InSameGroup(string r1, string r2, string[] group) =>
            group.Contains(r1) && group.Contains(r2);

        if (InSameGroup(guessRegion, targetRegion, upperBody) ||
            InSameGroup(guessRegion, targetRegion, arms) ||
            InSameGroup(guessRegion, targetRegion, lowerBody))
        {
            return MatchStatus.Partial;
        }

        return MatchStatus.Wrong;
    }

    private static MatchStatus GetCountMatchStatus(int guessCount, int targetCount)
    {
        if (guessCount == targetCount)
            return MatchStatus.Correct;

        // Close if within 2
        if (Math.Abs(guessCount - targetCount) <= 2)
            return MatchStatus.Partial;

        return MatchStatus.Wrong;
    }

    private static string FormatRegion(string region)
    {
        return region switch
        {
            "head" => "Head",
            "spine" => "Spine",
            "torso" => "Torso",
            "arm" => "Arm",
            "hand" => "Hand",
            "pelvis" => "Pelvis",
            "leg" => "Leg",
            "foot" => "Foot",
            _ => region
        };
    }

    private static string FormatBoneType(string boneType)
    {
        return boneType switch
        {
            "long" => "Long bone",
            "short" => "Short bone",
            "flat" => "Flat bone",
            "irregular" => "Irregular bone",
            "sesamoid" => "Sesamoid bone",
            _ => boneType
        };
    }
}
