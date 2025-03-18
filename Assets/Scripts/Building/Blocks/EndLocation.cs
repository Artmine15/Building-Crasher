using UnityEngine;
using VContainer;

namespace Artmine15.HappyBirthday.v3.Gisha
{
    public class EndLocation : Block
    {
        [Inject] private StagesSystem _stageSystem;

        [SerializeField] private StageProperties _endStage;

        public override void Activate()
        {
            base.Activate();
            if (State != BlockState.Waiting) return;

            RelatedStageProperties = _endStage;
            _stageSystem.Rest(RelatedStageProperties);
        }
    }
}
