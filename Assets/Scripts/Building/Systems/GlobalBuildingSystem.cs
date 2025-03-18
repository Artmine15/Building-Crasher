using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Artmine15.HappyBirthday.v3.Gisha
{
    public class GlobalBuildingSystem : MonoBehaviour
    {
        [Inject] private BlocksContainer _blocksContainer;
        [Inject] private BlocksTypeProvider _blocksTypeProvider;
        [Inject] private BuildingSystem _buildingSystem;
        [Inject] private BlocksDestributor _blocksDestributor;
        [Inject] private EntitiesContainer _entitiesContainer; public EntitiesContainer EntitiesContainer => _entitiesContainer;
        [Inject] private PlayerMovement _playerMovement;
        [Inject] private ParticlesSpawner _particlesSpawner;

        [SerializeField] private StageProperties[] _stages;
        
        private Vector2 _playerPlacePosition;

        public void Awake()
        {
            _blocksContainer.SpawnAllBlocks(GetAllBasicBlocksCount(), GetAllBrokenBlocksCount(), GetAllRestBlocksCount());
            _entitiesContainer.Initialize(_stages);
            _blocksTypeProvider.Initialize(_stages);
            _buildingSystem.Initialize(GetIterationsCount(), this);
        }

        private void OnEnable()
        {
            _blocksDestributor.OnNewCurrentBlockRequired += PlayFallParticleSystem;
        }

        private void OnDisable()
        {
            _blocksDestributor.OnNewCurrentBlockRequired -= PlayFallParticleSystem;
        }

        public void Start()
        {
            List<Block> _blocksPool;
            _buildingSystem.SpawnInitialBlock();
            _buildingSystem.SpawnAllBlocks(out _blocksPool);
            _blocksDestributor.Initialize(_blocksPool);
            _entitiesContainer.SpawnAllPossibleEntities();

            _playerPlacePosition = _buildingSystem.GetLastBlock().transform.position;
            _playerPlacePosition.y += _buildingSystem.GetLastBlock().GetSize().y / 3;
            _playerMovement.transform.position = _playerPlacePosition;
        }

        private void PlayFallParticleSystem()
        {
            _particlesSpawner.PlayParticleSystemOnce(_blocksDestributor.CurrentBlock.FallPSPrefab, _blocksDestributor.CurrentBlock.transform.position);
        }

        private int GetIterationsCount()
        {
            int sum = 0;
            for (int i = 0; i < _stages.Length; i++)
            {
                sum += _stages[i].BlocksSpawnProperties.GetAllBlocksCount() + GetAllRestBlocksCount();
            }
            return sum;
        }

        private int GetAllBasicBlocksCount()
        {
            int sum = 0;
            for (int i = 0; i < _stages.Length; i++)
            {
                sum += _stages[i].BlocksSpawnProperties.BasicBlocksCount;
            }
            return sum;
        }

        private int GetAllBrokenBlocksCount()
        {
            int sum = 0;
            for (int i = 0; i < _stages.Length; i++)
            {
                sum += _stages[i].BlocksSpawnProperties.BrokenBlocksCount;
            }
            return sum;
        }

        private int GetAllRestBlocksCount() => _stages.Length;
    }
}
