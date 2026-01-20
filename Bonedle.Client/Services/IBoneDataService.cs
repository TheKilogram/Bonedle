using Bonedle.Client.Models;

namespace Bonedle.Client.Services;

public interface IBoneDataService
{
    List<Bone> GetBones(DifficultyLevel difficulty);
    Bone? GetBoneById(string id);
    Bone? GetBoneByName(string name);
    int CalculateDistance(string fromBoneId, string toBoneId);
    string GetHeatColor(int distance);
    List<string> GetBoneNames(DifficultyLevel difficulty);
    List<string> SearchBones(string query, DifficultyLevel difficulty);
}
