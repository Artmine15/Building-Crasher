using TriInspector;
using UnityEngine;
using VContainer;

namespace Artmine15.HappyBirthday.v3.Gisha.DisplayDebug
{
    public class StatesDebug : MonoBehaviour
    {
        [Inject] private PlayerStates _playerStates;

        [SerializeField, ReadOnly] private bool IsMoving;
        [SerializeField, ReadOnly] private bool IsInJump;
        [SerializeField, ReadOnly] private bool IsOnGround;

        private void Update()
        {
            IsMoving = _playerStates.IsMoving;
            IsInJump = _playerStates.IsInJump;
            IsOnGround = _playerStates.IsOnGround;
        }
    }
}
