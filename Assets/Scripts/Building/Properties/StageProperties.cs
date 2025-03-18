using UnityEngine;

namespace Artmine15.HappyBirthday.v3.Gisha
{
    [CreateAssetMenu(fileName = "Stage ", menuName = "Stages Properties/Stage")]
    public class StageProperties : ScriptableObject
    {
        [SerializeField] private int _index; public int Index => _index;
        [Space]
        [SerializeField] private UnifiedBlockProperties _unifiedBlockProperties; public UnifiedBlockProperties UnifiedBlockProperties => _unifiedBlockProperties;
        [SerializeField] private BlocksSpawnProperties _blocksSpawnProperties; public BlocksSpawnProperties BlocksSpawnProperties => _blocksSpawnProperties;
        [SerializeField] private BasicBlockProperties _basicBlockProperties; public BasicBlockProperties BasicBlockProperties => _basicBlockProperties;
        [SerializeField] private BrokenBlockProperties _brokenBlockProperties; public BrokenBlockProperties BrokenBlockProperties => _brokenBlockProperties;
        [SerializeField] private MusicProperties _musicProperties; public MusicProperties MusicProperties => _musicProperties;
        [SerializeField] private SnowfallProperties _snowfallProperties; public SnowfallProperties SnowfallProperties => _snowfallProperties;
    }
}
