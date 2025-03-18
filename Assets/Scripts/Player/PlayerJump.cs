using Artmine15.Toolkit;
using UnityEngine;
using VContainer;

namespace Artmine15.HappyBirthday.v3.Gisha
{
    public class PlayerJump : MonoBehaviour
    {
        [Inject] private InputReceiver _inputReceiver;
        [Inject] private PlayerStates _playerStates;
        [Inject] private ParticlesSpawner _particlesSpawner;
        [Inject] private AudioHandler _audioHandler;
        [Inject] private PlayerGravity _playerGravity;

        [SerializeField] private CapsuleCollider2D _playerCollider;

        [SerializeField] private AnimationCurve _normalizedJumpCurve;
        [SerializeField] private float _curveTime;
        [SerializeField] private float _strengthOfJump;
        [SerializeField] private float _velocityAfterJump;

        [Space]
        [SerializeField] private Transform _view;
        private Vector2 _jumpPSLocalScale;
        [SerializeField] private ParticleSystem _jumpPSPrefab;

        private float _startYPosition;
        private float _yPositionDelta;

        [Space]
        [SerializeField] private int _jumpSFXPlayableChannel;

        private CommonTimer _jumpTimer = new CommonTimer();

        private void OnEnable()
        {
            _inputReceiver.OnJumpPerformed += TryStartJump;
        }

        private void OnDisable()
        {
            _inputReceiver.OnJumpPerformed -= TryStartJump;
        }

        private void Update()
        {
            _jumpTimer.Update(Time.deltaTime);

            if (_playerStates.IsInJump == true)
                Jump();
        }

        private void TryStartJump()
        {
            if (_playerStates.IsOnGround == false) return;

            _jumpTimer.Start(_curveTime, TimerGrowing.Increasing);

            _startYPosition = transform.position.y;
            _playerStates.IsInJump = true;
            _jumpPSLocalScale = _view.localScale; _jumpPSLocalScale.x *= -1;
            _particlesSpawner.PlayParticleSystemOnce(_jumpPSPrefab, new Vector2(transform.position.x, transform.TransformPoint(_playerCollider.offset).y - _playerCollider.size.y / 2), _jumpPSLocalScale);
            _audioHandler.PlaySFX(_jumpSFXPlayableChannel);

            _jumpTimer.OnEnded += ApplyFallingAfterJump;
        }

        private void Jump()
        {
            _yPositionDelta = _normalizedJumpCurve.Evaluate(_jumpTimer.GetTime());

            transform.position = new Vector2(transform.position.x, _startYPosition + _yPositionDelta * _strengthOfJump);
        }

        private void ApplyFallingAfterJump()
        {
            _jumpTimer.OnEnded -= ApplyFallingAfterJump;
            _playerStates.IsInJump = false;
            _playerGravity.SetVelocity(_velocityAfterJump);
        }
    }
}
