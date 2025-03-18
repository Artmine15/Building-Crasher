using UnityEngine;
using VContainer;

namespace Artmine15.HappyBirthday.v3.Gisha
{
    public class PlayerGravity : MonoBehaviour
    {
        [Inject] private PlayerStates _playerStates;
        [Inject] private Rigidbody2D _playerRigidbody;

        [SerializeField] private float _fallingMultiplier = 1;
        private float _fallingVelocity;

        private bool _isUsingGravity;

        private void Start()
        {
            ApplyGravity();
        }

        private void Update()
        {
            if(IsCanUseGravity() == true && _isUsingGravity == false)
                ApplyGravity();

            if (IsCanUseGravity() == false && _isUsingGravity == true)
                _isUsingGravity = false;

            if (_isUsingGravity == false) return;

            _fallingVelocity -= Time.deltaTime * _fallingMultiplier;
            transform.position = new Vector2(transform.position.x, transform.position.y + _fallingVelocity * Time.deltaTime);
        }

        private void ApplyGravity()
        {
            SetVelocity(0);
        }

        public void SetVelocity(float velocity)
        {
            _fallingVelocity = velocity;
            _playerRigidbody.linearVelocity = Vector2.zero;
            _isUsingGravity = true;
        }

        private bool IsCanUseGravity()
        {
            if (_playerStates.IsInJump == true) return false;
            if (_playerStates.IsOnGround == true) return false;

            return true;
        }
    }
}

