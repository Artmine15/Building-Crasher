using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Artmine15.HappyBirthday.v3.Gisha
{
    public class BuildingSystem
    {
        [Inject] private BlocksContainer _blocksContainer;
        [Inject] private BlocksTypeProvider _blocksTypeProvider;
        private GlobalBuildingSystem _globalBuildingSystem;
        [Inject] private AudioHandler _audioHandler;

        private BlockTypes _currentBlockType;
        private StageProperties _stageOnLastBlock;

        private List<BasicBlock> _spawnedBasicBlocks = new List<BasicBlock>();
        private List<BrokenBlock> _spawnedBrokenBlocks = new List<BrokenBlock>();
        private List<RestBlock> _spawnedRestBlocks = new List<RestBlock>();
        
        private List<Block> _builtBlocks = new List<Block>();

        private int _iterationsCount;
        private int _currentIteration;

        public void Initialize(int iterationsCount, GlobalBuildingSystem globalBuildingSystem)
        {
            _iterationsCount = iterationsCount;
            _globalBuildingSystem = globalBuildingSystem;
            _blocksContainer.GetSpawnedBlocksNonAlloc(out _spawnedBasicBlocks, out _spawnedBrokenBlocks, out _spawnedRestBlocks);
        }

        public void SpawnInitialBlock()
        {
            _currentBlockType = _blocksTypeProvider.GetNextBlock(out _stageOnLastBlock);
            switch (_currentBlockType)
            {
                case BlockTypes.Basic:
                    PlaceBlock(ref _spawnedBasicBlocks, false);
                    break;
                case BlockTypes.Broken:
                    PlaceBlock(ref _spawnedBrokenBlocks, false);
                    break;
            }
        }

        public void SpawnAllBlocks(out List<Block> builtBlocks)
        {
            for (int i = 0; i < _iterationsCount; i++)
            {
                _currentBlockType = _blocksTypeProvider.GetNextBlock(out _stageOnLastBlock);
                _currentIteration = i;
                switch (_currentBlockType)
                {
                    case BlockTypes.Basic:
                        PlaceBlock(ref _spawnedBasicBlocks, true);
                        break;
                    case BlockTypes.Broken:
                        PlaceBlock(ref _spawnedBrokenBlocks, false);
                        break;
                    case BlockTypes.Rest:
                        PlaceBlock(ref _spawnedRestBlocks, true);
                        break;
                    default:
                        break;
                }
            }

            builtBlocks = _builtBlocks;
        }

        private void PlaceBlock<T>(ref List<T> blocks, bool isRandomizeX) where T : Block
        {
            if (blocks.Count > 0)
            {
                Vector3 position;
                if (_builtBlocks.Count > 0)
                {
                    position = GetLastBlock().transform.position;
                }
                else
                {
                    position = Vector3.zero;
                    position.y -= blocks[0].GetSize().y;
                }

                SetBlockPositionFromPreviousPosition(blocks[0], position, isRandomizeX);
                blocks[0].Initialize(_stageOnLastBlock, _globalBuildingSystem, _audioHandler);
                blocks[0].IncreaseViewsSortOrder(-_currentIteration);
                _builtBlocks.Add(blocks[0]);
                blocks.RemoveAt(0);
            }
        }

        private void SetBlockPositionFromPreviousPosition(Block block, Vector3 previousBlockPosition, bool isRandomizeX)
        {
            if(isRandomizeX == true)
                block.transform.position = new Vector3(previousBlockPosition.x + Random.Range(-block.GetSize().x * 0.75f, block.GetSize().x * 0.75f), previousBlockPosition.y + block.GetSize().y, previousBlockPosition.z);
            else
                block.transform.position = new Vector3(previousBlockPosition.x, previousBlockPosition.y + block.GetSize().y, previousBlockPosition.z);
        }

        public Block GetLastBlock() => _builtBlocks[_builtBlocks.Count - 1];
    }
}
