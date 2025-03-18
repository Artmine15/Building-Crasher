using Unity.Cinemachine;
using UnityEngine;

namespace Artmine15.HappyBirthday.v3.Gisha
{
    public class FollowTarget : MonoBehaviour
    {
        [SerializeField] private UpdateMehod _updateMehod = UpdateMehod.Update;
        [SerializeField] private Transform _target;
        [SerializeField] private Vector2 _offset;

        private Vector3 _targetPosition;

        private void Update()
        {
            if (_updateMehod != UpdateMehod.Update) return;
            
            PlaceAtTarget();
        }

        private void FixedUpdate()
        {
            if (_updateMehod != UpdateMehod.FixedUpdate) return;

            PlaceAtTarget();
        }

        private void LateUpdate()
        {
            if (_updateMehod != UpdateMehod.LateUpdate) return;

            PlaceAtTarget();
        }

        private void PlaceAtTarget()
        {
            _targetPosition = _target.position + (Vector3)_offset;
            _targetPosition.z = transform.position.z;
            transform.position = _targetPosition;
        }
    }
}
