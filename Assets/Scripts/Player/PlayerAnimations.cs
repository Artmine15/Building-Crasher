using UnityEngine;
using VContainer;

namespace Artmine15.HappyBirthday.v3.Gisha
{
    public class PlayerAnimations : MonoBehaviour
    {
        [Inject] private InputReceiver _inputReceiver;

        [SerializeField] private Animator _bodyAnimator;
        [SerializeField] private Animator _legsAnimator;

        private void OnEnable()
        {
            _inputReceiver.OnMovePerformed += ApplyMoveAnimation;
            _inputReceiver.OnMoveCanceled += CancelMoveAnimation;
            _inputReceiver.OnAttackPerformed += PerformAttackAnimation;
        }

        private void OnDisable()
        {
            _inputReceiver.OnMovePerformed -= ApplyMoveAnimation;
            _inputReceiver.OnMoveCanceled -= CancelMoveAnimation;
            _inputReceiver.OnAttackPerformed -= PerformAttackAnimation;
        }

        private void ApplyMoveAnimation(Vector2 direction)
        {
            _bodyAnimator.SetBool("IsMoving", true);
            _legsAnimator.SetBool("IsMoving", true);
        }

        private void CancelMoveAnimation()
        {
            _bodyAnimator.SetBool("IsMoving", false);
            _legsAnimator.SetBool("IsMoving", false);
        }

        private void PerformAttackAnimation()
        {
            _bodyAnimator.SetTrigger("OnAttack");
        }
    }
}
