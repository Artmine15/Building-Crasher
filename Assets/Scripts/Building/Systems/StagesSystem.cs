using System;
using VContainer;

namespace Artmine15.HappyBirthday.v3.Gisha
{
    public class StagesSystem
    {
        [Inject] private AudioHandler _audioHandler;
        [Inject] private SnowfallSetuper _snowfall;

        private StageProperties _currentStage; public StageProperties CurrentStage => _currentStage;

        public event Action<StageProperties> OnRest;
        public event Action<StageProperties> OnMoveToNextStage;

        public void Rest(StageProperties blockStageProperties)
        {
            _currentStage = blockStageProperties;
            _audioHandler.PlayNewMusicOffset(_currentStage.MusicProperties.RestMusicStartOffset, _currentStage.MusicProperties.RestMusicEndOffset);
            _snowfall.SetNewSnowfall(_currentStage.SnowfallProperties);
            OnRest?.Invoke(_currentStage);
        }

        public void MoveToNextStage()
        {
            _audioHandler.PlayNewMusicOffset(_currentStage.MusicProperties.GameplayMusicStartOffset, _currentStage.MusicProperties.GameplayMusicEndOffset);
            _snowfall.ApplyNewSnowfall();
            OnMoveToNextStage?.Invoke(_currentStage);
        }
    }
}
