using Bonedle.Client.Data;
using Bonedle.Client.Models;

namespace Bonedle.Client.Services;

public class BoneDataService : IBoneDataService
{
    private readonly List<Bone> _allBones;
    private readonly Dictionary<string, Bone> _boneById;
    private readonly Dictionary<string, Bone> _boneByNameLower;

    public BoneDataService()
    {
        _allBones = BoneData.GetAllBones();
        _boneById = _allBones.ToDictionary(b => b.Id, b => b);
        _boneByNameLower = _allBones.ToDictionary(b => b.DisplayName.ToLowerInvariant(), b => b);
    }

    public List<Bone> GetBones(DifficultyLevel difficulty)
    {
        return difficulty == DifficultyLevel.Major
            ? _allBones.Where(b => b.IsMajorBone).ToList()
            : _allBones;
    }

    public Bone? GetBoneById(string id)
    {
        return _boneById.TryGetValue(id, out var bone) ? bone : null;
    }

    public Bone? GetBoneByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return null;

        var normalized = name.Trim().ToLowerInvariant();

        // Try exact match first
        if (_boneByNameLower.TryGetValue(normalized, out var bone))
            return bone;

        // Try ID match
        if (_boneById.TryGetValue(normalized, out bone))
            return bone;

        // Try partial match
        return _allBones.FirstOrDefault(b =>
            b.DisplayName.ToLowerInvariant().Contains(normalized) ||
            b.Id.Contains(normalized));
    }

    public int CalculateDistance(string fromBoneId, string toBoneId)
    {
        if (fromBoneId == toBoneId)
            return 0;

        // BFS to find shortest path
        var visited = new HashSet<string> { fromBoneId };
        var queue = new Queue<(string boneId, int distance)>();
        queue.Enqueue((fromBoneId, 0));

        while (queue.Count > 0)
        {
            var (currentId, distance) = queue.Dequeue();
            var currentBone = GetBoneById(currentId);

            if (currentBone == null)
                continue;

            foreach (var adjacentId in currentBone.AdjacentBones)
            {
                if (adjacentId == toBoneId)
                    return distance + 1;

                if (!visited.Contains(adjacentId))
                {
                    visited.Add(adjacentId);
                    queue.Enqueue((adjacentId, distance + 1));
                }
            }
        }

        // No path found - return max distance
        return int.MaxValue;
    }

    public string GetHeatColor(int distance)
    {
        return distance switch
        {
            0 => "#22c55e",     // Green - correct!
            1 => "#ef4444",     // Red - very hot
            2 => "#f97316",     // Orange - hot
            3 => "#eab308",     // Yellow - warm
            4 => "#fef08a",     // Light yellow - slightly warm
            _ => "#ffffff"      // White - cold
        };
    }

    public List<string> GetBoneNames(DifficultyLevel difficulty)
    {
        return GetBones(difficulty)
            .Select(b => b.DisplayName)
            .OrderBy(n => n)
            .ToList();
    }

    public List<string> SearchBones(string query, DifficultyLevel difficulty)
    {
        if (string.IsNullOrWhiteSpace(query))
            return new List<string>();

        var normalized = query.Trim().ToLowerInvariant();
        var bones = GetBones(difficulty);

        return bones
            .Where(b => b.DisplayName.ToLowerInvariant().Contains(normalized))
            .Select(b => b.DisplayName)
            .OrderBy(n => n.ToLowerInvariant().StartsWith(normalized) ? 0 : 1)
            .ThenBy(n => n)
            .Take(8)
            .ToList();
    }
}
