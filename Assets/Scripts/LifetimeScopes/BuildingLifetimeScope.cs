using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Artmine15.HappyBirthday.v3.Gisha
{
    public class BuildingLifetimeScope : LifetimeScope
    {
        [SerializeField] private BlocksContainer _blocksContainer;
        [SerializeField] private GlobalBuildingSystem _globalBuildingSystem;
        //[SerializeField] private EntitiesContainer _entitiesContainer;
        [SerializeField] private EndLocation _endLocation;
        [SerializeField] private CreditsActivator _creditsActivator;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(_blocksContainer);
            builder.RegisterComponent(_globalBuildingSystem);
            builder.RegisterComponent(_endLocation);
            builder.RegisterComponent(_creditsActivator);

            builder.Register<BlocksTypeProvider>(Lifetime.Singleton);
            builder.Register<BuildingSystem>(Lifetime.Singleton);
            builder.Register<EntitiesContainer>(Lifetime.Singleton);
        }
    }
}
