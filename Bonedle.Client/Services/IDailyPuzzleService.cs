using Bonedle.Client.Models;

namespace Bonedle.Client.Services;

public interface IDailyPuzzleService
{
    string GetDailyBoneId(GameMode mode, DifficultyLevel difficulty, DateTime date);
    string GetTodaysDate();
    int GetPuzzleNumber(DateTime date);
}
