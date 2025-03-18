using System;
using UnityEngine;
using VContainer;

namespace Artmine15.HappyBirthday.v3.Gisha
{
    public class PlayerFallDetector : MonoBehaviour
    {
        [Inject] private PlayerBlockActivator _playerBlockActivator;

        private Transform _transform => _playerBlockActivator.transform;
        private Block _lastTouchedBlock => _playerBlockActivator.LastTouchedBlock;
        private float _yDeathPosition;
        private bool _isFell = false;

        public event Action OnFell;

        private void OnEnable()
        {
            _playerBlockActivator.OnNewBlockTouched += SetBlockDeathPosition;
            _playerBlockActivator.OnLastTouchedBlockThrown += SetBelowBlockPosition;
        }

        private void OnDisable()
        {
            _playerBlockActivator.OnNewBlockTouched -= SetBlockDeathPosition;
            _playerBlockActivator.OnLastTouchedBlockThrown -= SetBelowBlockPosition;
        }

        public void Update()
        {
            if (_transform.position.y <= _yDeathPosition && _isFell == false)
            {
                //Debug.LogError($"{_transform.position.y}  {_yDeathPosition}");
                OnFell?.Invoke();
                _isFell = true;
            }
        }

        private void SetBlockDeathPosition()
        {
            _yDeathPosition = _lastTouchedBlock.transform.position.y - _lastTouchedBlock.GetSize().y;
            //Debug.Log($"SetBlockDeathPosition: {_yDeathPosition}");
        }

        private void SetBelowBlockPosition()
        {
            _yDeathPosition = _lastTouchedBlock.transform.position.y - _lastTouchedBlock.GetSize().y * 2;
            //Debug.Log($"SetBelowBlockPosition: {_yDeathPosition}");
        }
    }
}
