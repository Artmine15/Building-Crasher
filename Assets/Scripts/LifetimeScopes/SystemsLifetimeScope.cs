using Artmine15.SceneTransitionSystem.Components;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Artmine15.HappyBirthday.v3.Gisha
{
    public class SystemsLifetimeScope : LifetimeScope
    {
        [SerializeField] private InputReceiver _inputReceiver;
        [SerializeField] private AudioHandler _audioHandler;
        [SerializeField] private SnowfallSetuper _snowfallSetuper;
        [SerializeField] private SceneTransitionHandler _sceneTransitionHandler;
        [SerializeField] private BlocksDestributor _blocksDestributor;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(_inputReceiver);
            builder.RegisterComponent(_audioHandler);
            builder.RegisterComponent(_snowfallSetuper);
            builder.RegisterComponent(_sceneTransitionHandler);
            builder.RegisterComponent(_blocksDestributor);

            builder.Register<GameRestarter>(Lifetime.Singleton);
            builder.Register<StagesSystem>(Lifetime.Singleton);
            builder.Register<ParticlesSpawner>(Lifetime.Singleton);
        }
    }
}
