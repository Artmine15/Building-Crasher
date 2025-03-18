using Artmine15.Toolkit;
using UnityEngine;

namespace Artmine15.HappyBirthday.v3.Gisha
{
    public class Enemy : Entity
    {
        [SerializeField] private Rigidbody2D _enemyRigidbody;
        [SerializeField] private EnemyHealth _enemyHealth;
        private PlayerMovement _playerMovement;
        private PlayerStates _playerStates;

        [Space]
        [SerializeField] private UnifiedEnemyProperties _unifiedEnemyProperties;
        [SerializeField] private TypedEnemyProperties _enemyProperties;

        private Timer _knockoutTimer = new Timer();

        private float _moveDeltaSpeed;
        private Vector2 _targetPosition;
        private Vector2 _moveDirection;
        private int _knockoutCount;
        private float _currentBlockRightSideXPosition;
        private float _currentBlockLeftSideXPosition;

        private EnemyFallingState _fallingState;
        private bool _isNewTargeted;
        
        private float _defaultGravityScale;
        private float _fallingYWorldPosition;

        private void OnEnable()
        {
            _knockoutTimer.OnTimerEnded += DisableKnockout;
        }

        private void OnDisable()
        {
            _knockoutTimer.OnTimerEnded -= DisableKnockout;
        }

        public override void Initialize(EntitiesContainer entitiesContainer, ParticlesSpawner particlesSpawner)
        {
            base.Initialize(entitiesContainer, particlesSpawner);
            _defaultGravityScale = _enemyRigidbody.gravityScale;
            _moveDeltaSpeed = Random.Range(_enemyProperties.MinMoveDeltaSpeed, _enemyProperties.MaxMoveDeltaSpeed);
            _enemyHealth.Initialize(entitiesContainer, this);
        }

        public void Initialize(PlayerMovement playerMovement, PlayerStates playerStates)
        {
            _playerMovement = playerMovement;
            _playerStates = playerStates;
        }

        protected override void EnableEntity()
        {
            base.EnableEntity();
            _fallingState = EnemyFallingState.OnBlock;
        }

        public override void OnSpawn()
        {
            base.OnSpawn();
            _enemyHealth.RevertHealth();
            _isNewTargeted = false;
            _knockoutCount = 0;
            if (_defaultGravityScale != 0)
                DisableGravity();
            if (RelatedBlock != null)
                SetupDeathValues();
        }

        public override void SetupAfterSpawn(Block relatedBlock)
        {
            base.SetupAfterSpawn(relatedBlock);
            SetupDeathValues();
        }

        private void Update()
        {
            _knockoutTimer.UpdateTimer(Time.deltaTime);

            if (IsEntityEnabled == false) return;

            if (IsOutOfBlock() == true && _fallingState != EnemyFallingState.Fell)
            {
                StartFalling();
            }

            switch (_fallingState)
            {
                case EnemyFallingState.OnBlock:
                    if (_playerStates.IsInJump == false)
                    {
                        _isNewTargeted = true;
                        _enemyRigidbody.linearVelocity = Vector2.zero;
                        _targetPosition = _playerMovement.transform.position;
                        _targetPosition.y = transform.position.y;
                        _moveDirection = _targetPosition - (Vector2)transform.position; _moveDirection.y = 0; _moveDirection.Normalize();
                        transform.localScale = new Vector3(_moveDirection.x, transform.localScale.y, transform.localScale.z);
                    }
                    if(_isNewTargeted == true)
                        transform.position = Vector2.MoveTowards(transform.position, _targetPosition, _moveDeltaSpeed * Time.deltaTime);
                    break;
                case EnemyFallingState.Fell:
                    if (transform.position.y < _fallingYWorldPosition + _unifiedEnemyProperties.FallingYDistance)
                    {
                        ThisEntitiesContainer.DespawnEnemy(this);
                        _fallingState = EnemyFallingState.Killed;
                    }
                    break;
                default:
                    break;
            }
        }

        public void ApplyKnockout()
        {
            _knockoutCount++;
            _knockoutTimer.StartTimer(_unifiedEnemyProperties.KnockoutTime, TimerType.Common);
            EnableGravity();
            Vector2 direction = transform.position - _playerMovement.transform.position; direction.y = 0; direction.Normalize();
            direction = new Vector2(_enemyProperties.KnockoutDirection.x * direction.x, _enemyProperties.KnockoutDirection.y);
            _enemyRigidbody.AddForce(direction * _enemyProperties.StrengthOfKnockout / _knockoutCount, ForceMode2D.Impulse);
            _fallingState = EnemyFallingState.InAir;
        }

        private void DisableKnockout()
        {
            if (_fallingState == EnemyFallingState.Fell) return;

            _knockoutCount = 0;
            DisableGravity();
            _enemyRigidbody.linearVelocity = Vector2.zero;

            _fallingState = EnemyFallingState.OnBlock;
        }

        private void StartFalling()
        {
            EntityCollider.enabled = false;
            EnableGravity(true);

            _fallingState = EnemyFallingState.Fell;
        }

        private void EnableGravity(bool saveVelocity = false)
        {
            if(saveVelocity == false)
                _enemyRigidbody.linearVelocity = Vector2.zero;
            _enemyRigidbody.gravityScale = _defaultGravityScale;
            _enemyRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        private void DisableGravity()
        {
            _enemyRigidbody.gravityScale = 0;
            _enemyRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
        }

        private void SetupDeathValues()
        {
            _fallingYWorldPosition = RelatedBlock.transform.position.y + RelatedBlock.GetSize().y / 2 + Size.y / 2 - 1;
            _currentBlockRightSideXPosition = RelatedBlock.transform.position.x + RelatedBlock.GetSize().x / 2;
            _currentBlockLeftSideXPosition = RelatedBlock.transform.position.x - RelatedBlock.GetSize().x / 2;
        }

        private bool IsOutOfBlock()
        {
            if (transform.position.x < _currentBlockLeftSideXPosition ||
                transform.position.x > _currentBlockRightSideXPosition) return true;
            else return false;
        }
    }
}
