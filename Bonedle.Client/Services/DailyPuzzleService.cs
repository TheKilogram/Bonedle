using Bonedle.Client.Models;

namespace Bonedle.Client.Services;

public class DailyPuzzleService : IDailyPuzzleService
{
    private readonly IBoneDataService _boneDataService;
    private static readonly DateTime LaunchDate = new(2026, 1, 18);

    public DailyPuzzleService(IBoneDataService boneDataService)
    {
        _boneDataService = boneDataService;
    }

    public string GetDailyBoneId(GameMode mode, DifficultyLevel difficulty, DateTime date)
    {
        var bones = _boneDataService.GetBones(difficulty);
        var seed = GenerateSeed(mode, difficulty, date);
        var random = new Random(seed);

        // Fisher-Yates shuffle to pick a random bone
        var index = random.Next(bones.Count);
        return bones[index].Id;
    }

    private static int GenerateSeed(GameMode mode, DifficultyLevel difficulty, DateTime date)
    {
        // Create deterministic seed from date, mode, and difficulty
        var dateSeed = date.Year * 10000 + date.Month * 100 + date.Day;
        var modeOffset = (int)mode * 1000000;
        var difficultyOffset = (int)difficulty * 100000000;

        return dateSeed + modeOffset + difficultyOffset;
    }

    public string GetTodaysDate()
    {
        return DateTime.UtcNow.ToString("yyyy-MM-dd");
    }

    public int GetPuzzleNumber(DateTime date)
    {
        return (date.Date - LaunchDate.Date).Days + 1;
    }
}
