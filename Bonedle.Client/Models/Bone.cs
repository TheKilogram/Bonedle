namespace Bonedle.Client.Models;

public record Bone(
    string Id,
    string DisplayName,
    string Category,      // "axial" or "appendicular"
    string Region,        // "head", "spine", "torso", "arm", "hand", "pelvis", "leg", "foot"
    string BoneType,      // "long", "short", "flat", "irregular", "sesamoid"
    int CountInBody,
    List<string> AdjacentBones,
    bool IsMajorBone,
    double SvgX,          // X position for SVG display (0-100 scale)
    double SvgY           // Y position for SVG display (0-100 scale)
);
