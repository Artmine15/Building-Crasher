using Artmine15.Toolkit;
using UnityEngine;
using VContainer;

namespace Artmine15.HappyBirthday.v3.Gisha
{
    public class PlayerAttack : MonoBehaviour
    {
        [Inject] private InputReceiver _inputReceiver;
        [Inject] private AudioHandler _audioHandler;

        [SerializeField] private Vector2 _attackPoint;
        private Vector2 _setupedAttackPoint;
        private Vector2 _lastMovementDirection;
        [SerializeField] private float _attackRadius;
        private Collider2D[] _foundColliders;

        private CommonTimer _cooldownTimer = new CommonTimer();
        [Space]
        [SerializeField] private float _attackCooldownSeconds;

        [Space]
        [SerializeField] private int _attackSfxPlayableChannel;
        [SerializeField] private int _damageEnemySfxPlayableChannel;

        [Space]
        [SerializeField] private bool _isDrawDebugGizmos;

        private void OnEnable()
        {
            _inputReceiver.OnAttackPerformed += PerformAttack;
            _inputReceiver.OnMovePerformed += SetLastMovementDirection;
        }

        private void OnDisable()
        {
            _inputReceiver.OnAttackPerformed -= PerformAttack;
            _inputReceiver.OnMovePerformed -= SetLastMovementDirection;
        }

        private void Update()
        {
            _cooldownTimer.Update(Time.deltaTime);
        }

        private void PerformAttack()
        {
            if (_cooldownTimer.GetTime() > 0) return;

            SetAttackPoint();
            _foundColliders = Physics2D.OverlapCircleAll(_setupedAttackPoint, _attackRadius);
            for (int i = 0; i < _foundColliders.Length; i++)
            {
                if (_foundColliders[i].TryGetComponent(out Enemy enemy) == true)
                {
                    enemy.GetComponent<EnemyHealth>().ApplyDamage();
                    enemy.ApplyKnockout();
                    _audioHandler.PlaySFX(_damageEnemySfxPlayableChannel);
                }
            }
            _audioHandler.PlaySFX(_attackSfxPlayableChannel);
            _cooldownTimer.Start(_attackCooldownSeconds);
        }


        private void SetLastMovementDirection(Vector2 direction)
        {
            _lastMovementDirection = direction;
        }

        private void SetAttackPoint()
        {
            _setupedAttackPoint = (Vector2)transform.position + _attackPoint;
            _setupedAttackPoint.x = transform.position.x + _attackPoint.x * _lastMovementDirection.x;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (_isDrawDebugGizmos == false) return;

            Gizmos.DrawSphere(_setupedAttackPoint, _attackRadius);
        }
#endif
    }
}
