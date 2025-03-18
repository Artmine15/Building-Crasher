using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Artmine15.HappyBirthday.v3.Gisha
{
    public class InputReceiver : MonoBehaviour
    {
        [SerializeField] private InputActionAsset _actionAsset;
        private InputAction _moveAction;
        private InputAction _jumpAction;
        private InputAction _attackAction;

        [SerializeField] private UIDocument _touchscreenControlsDocument;
        private VisualElement _root;
        private Button _moveLeftButton;
        private Button _moveRightButton;
        private Button _jumpButton;
        private Button _attackButton;

        //[Space]
        //[SerializeField] private PointerEventsReceiver _moveLeftButton;
        //[SerializeField] private PointerEventsReceiver _moveRightButton;
        //[SerializeField] private PointerEventsReceiver _jumpButton;
        //[SerializeField] private PointerEventsReceiver _attackButton;

        public event Action<Vector2> OnMovePerformed;
        public event Action OnMoveCanceled;
        public event Action OnJumpPerformed;
        public event Action OnAttackPerformed;

        private void Awake()
        {
            _moveAction = _actionAsset.FindAction("Move");
            _jumpAction = _actionAsset.FindAction("Jump");
            _attackAction = _actionAsset.FindAction("Attack");

            if (_touchscreenControlsDocument.enabled == false) return;

            _root = _touchscreenControlsDocument.rootVisualElement;
            _moveLeftButton = _root.Q<Button>("MoveLeft");
            _moveRightButton = _root.Q<Button>("MoveRight");
            _jumpButton = _root.Q<Button>("Jump");
            _attackButton = _root.Q<Button>("Attack");
        }

        private void OnEnable()
        {
            RegisterInput();
        }

        private void RegisterInput()
        {
            _moveAction.performed += (context) => OnMovePerformed?.Invoke(_moveAction.ReadValue<Vector2>());
            _moveAction.canceled += (context) => OnMoveCanceled?.Invoke();
            _jumpAction.performed += (context) => OnJumpPerformed?.Invoke();
            _attackAction.performed += (context) => OnAttackPerformed?.Invoke();

            //_moveLeftButton.OnPointerDownEvent += () => OnMovePerformed?.Invoke(Vector2.left);
            //_moveLeftButton.OnPointerUpEvent += () => OnMoveCanceled?.Invoke();
            //_moveRightButton.OnPointerDownEvent += () => OnMovePerformed?.Invoke(Vector2.right);
            //_moveRightButton.OnPointerUpEvent += () => OnMoveCanceled?.Invoke();
            //_jumpButton.OnPointerDownEvent += () => OnJumpPerformed?.Invoke();
            //_attackButton.OnPointerDownEvent += () => OnAttackPerformed?.Invoke();

            if (_touchscreenControlsDocument.enabled == false) return;

            _moveLeftButton.RegisterCallback<PointerDownEvent>((evt) => OnMovePerformed?.Invoke(Vector2.left), TrickleDown.TrickleDown);
            _moveLeftButton.RegisterCallback<PointerUpEvent>((evt) => OnMoveCanceled?.Invoke());
            _moveRightButton.RegisterCallback<PointerDownEvent>((evt) => OnMovePerformed?.Invoke(Vector2.right), TrickleDown.TrickleDown);
            _moveRightButton.RegisterCallback<PointerUpEvent>((evt) => OnMoveCanceled?.Invoke());
            _jumpButton.RegisterCallback<PointerDownEvent>((evt) => OnJumpPerformed?.Invoke(), TrickleDown.TrickleDown);
            _attackButton.RegisterCallback<PointerDownEvent>((evt) => OnAttackPerformed?.Invoke(), TrickleDown.TrickleDown);
        }

        public void DisableInput()
        {
            _moveAction.Disable();
            _jumpAction.Disable();
            _attackAction.Disable();
        }
    }
}
