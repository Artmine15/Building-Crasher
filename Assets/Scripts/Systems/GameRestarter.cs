using Artmine15.SceneTransitionSystem.Components;
using VContainer;

namespace Artmine15.HappyBirthday.v3.Gisha
{
    public class GameRestarter
    {
        [Inject] private SceneTransitionHandler _sceneTransitionHandler;

        public void Restart()
        {
            _sceneTransitionHandler.DoAllTransitionsIn();
        }
    }
}
