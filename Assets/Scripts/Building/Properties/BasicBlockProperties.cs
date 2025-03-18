using UnityEngine;

namespace Artmine15.HappyBirthday.v3.Gisha
{
    [CreateAssetMenu(fileName ="Basic Block Properties", menuName = "Stages Properties/Basic Block")]
    public class BasicBlockProperties : ScriptableObject
    {
        [SerializeField] private int _enemySpawnIterationsCount; public int EnemySpawnIterationsCount => _enemySpawnIterationsCount;
        [SerializeField, Range(0, 100)] private int _chanceOfSpawnEnemyOnIteration; public int ChanceOfSpawnEnemyOnIteration => _chanceOfSpawnEnemyOnIteration;

        [SerializeField] private Enemy[] _enemiesPrefabs; public Enemy[] EnemiesPrefabs => _enemiesPrefabs;

        [Space]
        [SerializeField] private int _spikesSpawnIterationsCount; public int SpikesSpawnIterationsCount => _spikesSpawnIterationsCount;
        [SerializeField, Range(0, 100)] private int _chanceOfSpawnSpikeOnIteration; public int ChanceOfSpawnSpikeOnIteration => _chanceOfSpawnSpikeOnIteration;
        [SerializeField] private Spike[] _spikesPrefabs; public Spike[] SpikesPrefabs => _spikesPrefabs;

        public int GetAllPossibleEntitiesCount() => _enemySpawnIterationsCount + _spikesSpawnIterationsCount;
    }
}
