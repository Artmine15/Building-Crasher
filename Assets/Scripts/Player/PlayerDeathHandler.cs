using VContainer;
using VContainer.Unity;

namespace Artmine15.HappyBirthday.v3.Gisha
{
    public class PlayerDeathHandler : IInitializable
    {
        [Inject] private GameRestarter _gameRestarter;
        [Inject] private PlayerHealth _health;
        [Inject] private PlayerFallDetector _playerFallDetector;
        [Inject] private PlayerBlockActivator _blockActivator;

        public void Initialize()
        {
            _playerFallDetector.OnFell += Kill;
            _health.OnHealthIsOver += Kill;
            _blockActivator.OnPlayerTouchedBlockWhenCannotActivate += Kill;
        }

        private void UnsubscribeAllEvents()
        {
            _playerFallDetector.OnFell -= Kill;
            _health.OnHealthIsOver -= Kill;
            _blockActivator.OnPlayerTouchedBlockWhenCannotActivate -= Kill;
        }

        private void Kill()
        {
            _gameRestarter.Restart();
            UnsubscribeAllEvents();
        }
    }
}
