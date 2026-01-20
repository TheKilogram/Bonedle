namespace Bonedle.Client.Models;

public record GuessResult(
    string BoneId,
    string BoneName,
    bool IsCorrect,
    int Distance,         // Distance from target (0 = correct)
    string HeatColor      // CSS color based on distance
);
