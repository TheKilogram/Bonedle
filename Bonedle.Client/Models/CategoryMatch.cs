namespace Bonedle.Client.Models;

public enum MatchStatus
{
    Correct,    // Green - exact match
    Partial,    // Yellow - partially correct (e.g., same region group)
    Wrong       // Red - completely wrong
}

public record CategoryMatch(
    string CategoryName,
    string GuessValue,
    string TargetValue,
    MatchStatus Status
);

public record NameGuessResult(
    string BoneId,
    string BoneName,
    bool IsCorrect,
    CategoryMatch CategoryMatch,     // Axial vs Appendicular
    CategoryMatch RegionMatch,       // Head, Spine, Torso, Arm, Hand, Pelvis, Leg, Foot
    CategoryMatch BoneTypeMatch,     // Long, Short, Flat, Irregular, Sesamoid
    CategoryMatch CountMatch         // Count in body (exact, close, far)
);
