using UnityEngine;
using VContainer;

namespace Artmine15.HappyBirthday.v3.Gisha
{
    public class PlayerMovement : MonoBehaviour
    {
        [Inject] private InputReceiver _inputReceiver;
        [Inject] private PlayerStates _playerStates;
        [Inject] private Rigidbody2D _playerRigidbody;

        [SerializeField] private CapsuleCollider2D _playerCollider;

        private Vector2 _moveDirection;
        [Space]
        [SerializeField] private float _moveDeltaSpeed;

        [Space]
        [SerializeField] private Transform _view;
        [SerializeField] private ParticleSystem _movePS;
        private ParticleSystem.EmissionModule _movePSEmission;
        private Vector2 _movePSLocalScale;

        private void Awake()
        {
            _movePSEmission = _movePS.emission;
            DisableMoveParticles();
        }

        private void OnEnable()
        {
            _inputReceiver.OnMovePerformed += EnableMovement;
            _inputReceiver.OnMoveCanceled += DisableMovement;
        }

        private void OnDisable()
        {
            _inputReceiver.OnMovePerformed -= EnableMovement;
            _inputReceiver.OnMoveCanceled -= DisableMovement;
        }

        private void EnableMovement(Vector2 direction)
        {
            _moveDirection = direction;
            _view.localScale = new Vector3(-direction.x, _view.localScale.y, _view.localScale.z);
            _playerRigidbody.linearVelocityX = 0;
            EnableMoveParticles();
            _playerStates.IsMoving = true;
        }

        private void DisableMovement()
        {
            DisableMoveParticles();
            _playerStates.IsMoving = false;
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            if (_playerStates.IsMoving == false) return;

            transform.Translate(_moveDirection * _moveDeltaSpeed * Time.deltaTime);

            _movePSLocalScale.x = _moveDirection.x;
            _movePSLocalScale.y = _movePS.transform.localScale.y;
            _movePS.transform.localScale = _movePSLocalScale;

            if (_playerStates.IsOnGround == false && _movePS.isEmitting == true)
                DisableMoveParticles();

            if (_playerStates.IsOnGround == true && _movePS.isEmitting == false)
                EnableMoveParticles();
        }

        private void EnableMoveParticles()
        {
            _movePSEmission.enabled = true;
        }

        private void DisableMoveParticles()
        {
            _movePSEmission.enabled = false;
        }
    }
}
