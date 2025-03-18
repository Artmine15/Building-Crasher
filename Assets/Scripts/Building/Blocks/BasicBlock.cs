using System.Collections.Generic;
using UnityEngine;

namespace Artmine15.HappyBirthday.v3.Gisha
{
    public class BasicBlock : Block
    {
        [Space]
        [SerializeField] private GameObject[] _views;

        [Space]
        [SerializeField] private float _spawnXPositionRandomness; public float XPositionRandomness => _spawnXPositionRandomness;

        private float[] _xLocalPositions = new float[4]; public float[] XLocalPositions => _xLocalPositions;
        private float[] _xWorldPositions = new float[4]; public float[] XWorldPositions => _xWorldPositions;

        public override void Initialize(StageProperties stage, GlobalBuildingSystem globalBuildingSystem, AudioHandler audioHandler)
        {
            base.Initialize(stage, globalBuildingSystem, audioHandler);
            _xLocalPositions[0] = GetSize().x / 8;
            _xLocalPositions[1] = GetSize().x / 8 * 3;
            _xLocalPositions[2] = -_xLocalPositions[0];
            _xLocalPositions[3] = -_xLocalPositions[1];

            _xWorldPositions[0] = transform.position.x + (GetSize().x / 8);
            _xWorldPositions[1] = transform.position.x + (GetSize().x / 8 * 3);
            _xWorldPositions[2] = -_xWorldPositions[0];
            _xWorldPositions[3] = -_xWorldPositions[1];

            foreach (var item in _views)
            {
                item.SetActive(false);
            }
            _views[Random.Range(0, _views.Length - 1)].SetActive(true);
        }

        public override void Activate()
        {
            base.Activate();
            if (State != BlockState.Waiting) return;

            GlobalBuildingSystem.EntitiesContainer.SpawnEntitiesOnBlock(this, RelatedStageProperties);
        }

        public override void StartFalling()
        {
            base.StartFalling();
            DisableAllColliders();
        }
    }
}

