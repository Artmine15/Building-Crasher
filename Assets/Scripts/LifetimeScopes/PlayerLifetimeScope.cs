using Artmine15.HappyBirthday.v3.Gisha.DisplayDebug;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Artmine15.HappyBirthday.v3.Gisha
{
    public class PlayerLifetimeScope : LifetimeScope
    {
        [SerializeField] private Rigidbody2D _playerRigidbody;

        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerJump _playerJump;
        [SerializeField] private PlayerGroundDetector _playerGroundDetector;
        [SerializeField] private PlayerGravity _gravity;
        [SerializeField] private PlayerAttack _playerAttack;
        [SerializeField] private PlayerBlockActivator _blockActivator;
        [SerializeField] private PlayerHealth _health;
        [SerializeField] private PlayerCameraFollow _cameraFollow;
        //[SerializeField] private PlayerCameraFollowHandler _cameraFollowHandler;
        [SerializeField] private PlayerFallDetector _fallDetector;
        [SerializeField] private PlayerAnimations _animations;

        [SerializeField] private StatesDebug _statesDebug;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<PlayerStates>(Lifetime.Singleton);

            builder.RegisterComponent(_playerRigidbody);

            builder.RegisterComponent(_playerMovement);
            builder.RegisterComponent(_playerJump);
            builder.RegisterComponent(_playerGroundDetector);
            builder.RegisterComponent(_gravity);
            builder.RegisterComponent(_playerAttack);
            builder.RegisterComponent(_blockActivator);
            builder.RegisterComponent(_health);
            builder.RegisterComponent(_cameraFollow);
            //builder.RegisterComponent(_cameraFollowHandler);
            builder.RegisterComponent(_fallDetector);
            builder.RegisterComponent(_animations);

            //builder.Register<PlayerFallDetector>(Lifetime.Singleton);
            //builder.RegisterEntryPoint<PlayerFallDetector>();
            builder.Register<PlayerDeathHandler>(Lifetime.Singleton);
            builder.RegisterEntryPoint<PlayerDeathHandler>();

            builder.RegisterComponent(_statesDebug);
        }
    }
}
