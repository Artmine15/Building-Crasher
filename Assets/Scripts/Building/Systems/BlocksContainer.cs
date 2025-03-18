using System.Collections.Generic;
using UnityEngine;

namespace Artmine15.HappyBirthday.v3.Gisha
{
    public class BlocksContainer : MonoBehaviour
    {
        [SerializeField] private BasicBlock _basicBlockPerfab;
        private List<BasicBlock> _spawnedBasicBlocks = new List<BasicBlock>();
        [SerializeField] private Transform _basicBlocksContainer;

        [Space]
        [SerializeField] private BrokenBlock _brokenBlockPerfab;
        private List<BrokenBlock> _spawnedBrokenBlocks = new List<BrokenBlock>();
        [SerializeField] private Transform _brokenBlocksContainer;

        [Space]
        [SerializeField] private RestBlock _restBlockPerfab;
        private List<RestBlock> _spawnedRestBlocks = new List<RestBlock>();
        [SerializeField] private Transform _restBlocksContainer;

        public void SpawnAllBlocks(int basicBlocksCount, int brokenBlocksCount, int restBlocksCount)
        {
            for (int i = 0; i < basicBlocksCount; i++)
                _spawnedBasicBlocks.Add(Instantiate(_basicBlockPerfab, _basicBlocksContainer));

            for (int i = 0; i < brokenBlocksCount; i++)
                _spawnedBrokenBlocks.Add(Instantiate(_brokenBlockPerfab, _brokenBlocksContainer));

            for (int i = 0; i < restBlocksCount; i++)
                _spawnedRestBlocks.Add(Instantiate(_restBlockPerfab, _restBlocksContainer));
        }

        public void GetSpawnedBlocksNonAlloc(out List<BasicBlock> spawnedBasicBlocks, out List<BrokenBlock> spawnedBrokenBlocks, out List<RestBlock> spawnedRestBlocks)
        {
            spawnedBasicBlocks = new List<BasicBlock>();
            spawnedBrokenBlocks = new List<BrokenBlock>();
            spawnedRestBlocks = new List<RestBlock>();

            spawnedBasicBlocks = _spawnedBasicBlocks;
            spawnedBrokenBlocks = _spawnedBrokenBlocks;
            spawnedRestBlocks = _spawnedRestBlocks;
        }
    }
}
