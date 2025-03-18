using Artmine15.Extensions;
using Artmine15.Toolkit;

namespace Artmine15.HappyBirthday.v3.Gisha
{
    public class BlocksTypeProvider
    {
        private StageProperties[] _stages;
        private StageProperties _currentStage;

        private BlocksSpawnProperties _currentBlocksSpawn => _currentStage.BlocksSpawnProperties;

        private BlockTypes _lastSpawnedBlockType;

        private int _currentBasicBlocksCount;
        private int _currentBrokenBlocksCount;

        public void Initialize(StageProperties[] stages)
        {
            _stages = stages;
            SetNewCurrentStage(_stages[0]);
        }

        public BlockTypes GetNextBlock(out StageProperties stageProperties)
        {
            if (GetAllCurrentBlocksCount() > 0)
            {
                if (_lastSpawnedBlockType == BlockTypes.Broken)
                {
                    stageProperties = _currentStage;
                    return PlaceBasicBlock();
                }

                if (_currentBasicBlocksCount > _currentBrokenBlocksCount)
                {
                    if (RandomExtensions.ChanceOf(_currentBlocksSpawn.ChanceOfBrokenBlockSpawn) == true && _currentBrokenBlocksCount > 0)
                    {
                        stageProperties = _currentStage;
                        return PlaceBrokenBlock();
                    }
                    else
                    {
                        stageProperties = _currentStage;
                        return PlaceBasicBlock();
                    }
                }
                else if (_currentBasicBlocksCount == _currentBrokenBlocksCount ||
                    _currentBasicBlocksCount < _currentBrokenBlocksCount)
                {
                    stageProperties = _currentStage;
                    return PlaceBrokenBlock();
                }
                else
                {
                    stageProperties = _currentStage;
                    return PlaceBasicBlock();
                }
            }
            else
            {
                stageProperties = _currentStage;
                MoveToNextStage();
                return BlockTypes.Rest;
            }
        }

        public BlockTypes PlaceBasicBlock()
        {
            _currentBasicBlocksCount--;
            _lastSpawnedBlockType = BlockTypes.Basic;
            return BlockTypes.Basic;
        }

        public BlockTypes PlaceBrokenBlock()
        {
            _currentBrokenBlocksCount--;
            _lastSpawnedBlockType = BlockTypes.Broken;
            return BlockTypes.Broken;
        }

        public void MoveToNextStage()
        {
            for (int i = 0; i < _stages.Length; i++)
            {
                if (_stages[i].Index > _currentStage.Index)
                {
                    SetNewCurrentStage(_stages[i]);
                    return;
                }
                else
                {
                    continue;
                }
            }
        }

        private void SetNewCurrentStage(StageProperties newProperties)
        {
            _currentStage = newProperties;
            _currentBasicBlocksCount = _currentBlocksSpawn.BasicBlocksCount;
            _currentBrokenBlocksCount = _currentBlocksSpawn.BrokenBlocksCount;
        }

        public int GetAllCurrentBlocksCount() => _currentBasicBlocksCount + _currentBrokenBlocksCount;
    }
}
