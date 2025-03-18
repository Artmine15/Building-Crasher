using Artmine15.Toolkit;
using NTC.Pool;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Artmine15.HappyBirthday.v3.Gisha
{
    public class EntitiesContainer
    {
        [Inject] private PlayerMovement _playerMovement;
        [Inject] private PlayerStates _playerStates;
        [Inject] private ParticlesSpawner _particlesSpawner;

        private StageProperties[] _stages;

        private Vector2 _tempEntityPosition;
        private Entity[] _initializedEntities;
        private List<Entity> _currentSpawnedEntities = new List<Entity>();
        private Enemy _lastEnemy;
        private Spike _lastSpike;
        private BasicBlock _currentBasicBlock;
        private int _countOfEnemies;

        public void Initialize(StageProperties[] stages)
        {
            _stages = stages;
        }

        public void SpawnAllPossibleEntities()
        {
            for (int i = 0; i < _stages.Length; i++)
            {
                for (int j = 0; j < _stages[i].BasicBlockProperties.EnemiesPrefabs.Length; j++)
                {
                    _initializedEntities = new Entity[_stages[i].BasicBlockProperties.EnemySpawnIterationsCount];
                    for (int k = 0; k < _stages[i].BasicBlockProperties.EnemySpawnIterationsCount; k++)
                    {
                        _lastEnemy = NightPool.Spawn(
                            _stages[i].BasicBlockProperties.EnemiesPrefabs[j]);

                        _initializedEntities[k] = _lastEnemy;

                        _lastEnemy.Initialize(this, _particlesSpawner);
                        _lastEnemy.Initialize(_playerMovement, _playerStates);
                    }

                    for (int k = 0; k < _stages[i].BasicBlockProperties.EnemySpawnIterationsCount; k++)
                        NightPool.Despawn(_initializedEntities[k]);
                }

                for (int j = 0; j < _stages[i].BasicBlockProperties.SpikesPrefabs.Length; j++)
                {
                    _initializedEntities = new Entity[_stages[i].BasicBlockProperties.SpikesSpawnIterationsCount];
                    for (int k = 0; k < _stages[i].BasicBlockProperties.SpikesSpawnIterationsCount; k++)
                    {
                        _lastSpike = NightPool.Spawn(
                            _stages[i].BasicBlockProperties.SpikesPrefabs[j]);

                        _initializedEntities[k] = _lastSpike;

                        _lastSpike.Initialize(this, _particlesSpawner);
                    }

                    for (int k = 0; k < _stages[i].BasicBlockProperties.SpikesSpawnIterationsCount; k++)
                        NightPool.Despawn(_initializedEntities[k]);
                }
            }
        }

        public void SpawnEntitiesOnBlock(BasicBlock basicBlock, StageProperties currentProperties)
        {
            _currentBasicBlock = basicBlock;
            SpawnEnemy(_currentBasicBlock, currentProperties);

            for (int i = 0; i < currentProperties.BasicBlockProperties.EnemySpawnIterationsCount - 1; i++)
            {
                if (RandomExtensions.ChanceOf(currentProperties.BasicBlockProperties.ChanceOfSpawnEnemyOnIteration) == true)
                {
                    SpawnEnemy(_currentBasicBlock, currentProperties);
                }
            }

            for (int i = 0; i < currentProperties.BasicBlockProperties.SpikesSpawnIterationsCount; i++)
            {
                if (RandomExtensions.ChanceOf(currentProperties.BasicBlockProperties.ChanceOfSpawnSpikeOnIteration) == true)
                {
                    _lastSpike = currentProperties.BasicBlockProperties.SpikesPrefabs[Random.Range(0, currentProperties.BasicBlockProperties.SpikesPrefabs.Length)];
                    SetupEntity(_lastSpike, _currentBasicBlock);
                }
            }

            _currentBasicBlock.OnThrown += DisableAllEntitiesOnCurrentBlock;
        }

        private void SpawnEnemy(BasicBlock basicBlock, StageProperties currentProperties)
        {
            _lastEnemy = currentProperties.BasicBlockProperties.EnemiesPrefabs[Random.Range(0, currentProperties.BasicBlockProperties.EnemiesPrefabs.Length)];
            SetupEntity(_lastEnemy, basicBlock);
            _countOfEnemies++;
        }

        private void SetupEntity(Entity prefab, BasicBlock basicBlock)
        {
            Entity entity = NightPool.Spawn(prefab);
            entity.SetupAfterSpawn(basicBlock);
            entity.transform.SetParent(basicBlock.transform);
            PlaceEntityOnBlockByGrid(basicBlock, entity);
            _currentSpawnedEntities.Add(entity);
        }

        private void PlaceEntityOnBlockByGrid(BasicBlock basicBlock, Entity entity)
        {
            _tempEntityPosition.x = basicBlock.XLocalPositions[Random.Range(0, basicBlock.XLocalPositions.Length)] + Random.Range(-basicBlock.XPositionRandomness, basicBlock.XPositionRandomness);
            _tempEntityPosition.y = basicBlock.transform.position.y + (basicBlock.GetSize().y / 2) + (entity.ObjectSize.y / 2);
            entity.transform.position = new Vector2(entity.transform.position.x, _tempEntityPosition.y);
            entity.transform.localPosition = new Vector2(_tempEntityPosition.x, entity.transform.localPosition.y);
        }

        private void DisableAllEntitiesOnCurrentBlock()
        {
            for (int i = 0; i < _currentSpawnedEntities.Count; i++)
            {
                NightPool.Despawn(_currentSpawnedEntities[i]);
            }

            _currentSpawnedEntities.Clear();
            _currentBasicBlock.OnThrown -= DisableAllEntitiesOnCurrentBlock;
        }

        public void DespawnEnemy(Enemy clone)
        {
            DespawnEntity(clone);

            if (_countOfEnemies > 0)
            {
                _countOfEnemies--;
                if (_countOfEnemies <= 0)
                    _currentBasicBlock.Throw();
            }
            else
            {
                _currentBasicBlock.Throw();
            }
        }

        private void DespawnEntity(Entity clone)
        {
            NightPool.Despawn(clone);
            _currentSpawnedEntities.Remove(clone);
        }
    }
}
