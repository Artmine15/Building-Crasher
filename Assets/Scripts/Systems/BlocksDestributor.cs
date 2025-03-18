using System;
using System.Collections.Generic;
using UnityEngine;

namespace Artmine15.HappyBirthday.v3.Gisha
{
    public class BlocksDestributor : MonoBehaviour
    {
        [SerializeField] private EndLocation _endBlock;

        private List<Block> _blocksPool;
        public Block PreviousBlock { get; private set; }
        public Block CurrentBlock { get; private set; }
        public Block NextBlock { get; private set; }

        public event Action OnNewCurrentBlockRequired;
        public event Action OnNewCurrentBlockSet;

        public void Initialize(List<Block> pool)
        {
            _blocksPool = pool;
            _blocksPool.Insert(0, _endBlock);
            CurrentBlock = _blocksPool[_blocksPool.Count - 1];
            CurrentBlock.MakeWaiting();
            NextBlock = _blocksPool[_blocksPool.Count - 2];
            CurrentBlock.OnThrown += MoveToNextBlock;
        }

        private void MoveToNextBlock()
        {
            CurrentBlock.OnThrown -= MoveToNextBlock;

            OnNewCurrentBlockRequired?.Invoke();

            if(CurrentBlock != null)
            {
                PreviousBlock = CurrentBlock;
                _blocksPool.Remove(CurrentBlock);
            }
            else
            {
                PreviousBlock = null;
            }

            if (NextBlock != null)
            {
                CurrentBlock = NextBlock;
                CurrentBlock.MakeWaiting();
                OnNewCurrentBlockSet?.Invoke();
                CurrentBlock.OnThrown += MoveToNextBlock;
            }
            else
            {
                CurrentBlock = null;
            }

            if (_blocksPool.Count > 1)
                NextBlock = _blocksPool[_blocksPool.Count - 2];
            else NextBlock = null;
        }
    }
}
