using UnityEngine;

namespace Artmine15.HappyBirthday.v3.Gisha
{
    public class BrokenBlock : Block
    {
        private BrokenBlockProperties _brokenBlockProperties => RelatedStageProperties.BrokenBlockProperties;

        public override void Initialize(StageProperties stage, GlobalBuildingSystem globalBuildingSystem, AudioHandler audioHandler)
        {
            base.Initialize(stage, globalBuildingSystem, audioHandler);
            BrokenBlockPart spawnedPart = Instantiate(_brokenBlockProperties.Parts[Random.Range(0, _brokenBlockProperties.Parts.Length)], transform);
            spawnedPart.Initialize(this);
            AddColorableSprite(spawnedPart.SpriteRenderer);
            for (int i = 0; i < spawnedPart.Colliders.Length; i++)
                AddBoxCollider(spawnedPart.Colliders[i]);

            for (int i = 0; i < spawnedPart.Lights.Length; i++)
                AddLight(spawnedPart.Lights[i]);
        }
        public override void Activate()
        {
            base.Activate();
            if (State != BlockState.Waiting) return;

            DisableAllColliders();
            Throw();
        }
    }
}
