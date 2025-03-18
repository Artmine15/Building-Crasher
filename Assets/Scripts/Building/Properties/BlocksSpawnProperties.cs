using UnityEngine;

namespace Artmine15.HappyBirthday.v3.Gisha
{
    [CreateAssetMenu(fileName = "Blocks Spawn Properties", menuName = "Stages Properties/Blocks Spawn")]
    public class BlocksSpawnProperties : ScriptableObject
    {
        [SerializeField] private int _basicBlocksCount; public int BasicBlocksCount => _basicBlocksCount;
        [SerializeField] private int _brokenBlocksCount; public int BrokenBlocksCount => _brokenBlocksCount;
        [SerializeField] private int _chanceOfBrokenBlockSpawn; public int ChanceOfBrokenBlockSpawn => _chanceOfBrokenBlockSpawn;

        public int GetAllBlocksCount() => _basicBlocksCount + _brokenBlocksCount;
    }
}
