using Unity.Cinemachine;
using UnityEngine;
using VContainer;

namespace Artmine15.HappyBirthday.v3.Gisha
{
    public class PlayerCameraFollow : MonoBehaviour
    {
        [Inject] private BlocksDestributor _blocksDestributor;

        [SerializeField] private CinemachineTargetGroup _targetGroup;
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private float _addedMemberRadius;
        [SerializeField] private float _addedMemberWeight;

        private void OnEnable()
        {
            _blocksDestributor.OnNewCurrentBlockSet += AddNewTarget;
        }

        private void OnDisable()
        {
            _blocksDestributor.OnNewCurrentBlockSet -= AddNewTarget;
        }

        private void AddNewTarget()
        {
            _targetGroup.AddMember(_blocksDestributor.CurrentBlock.transform, _addedMemberWeight, _addedMemberRadius);
            _blocksDestributor.CurrentBlock.OnActivated += RemoveTarget;
        }

        private void RemoveTarget()
        {
            _blocksDestributor.CurrentBlock.OnActivated -= RemoveTarget;
            _targetGroup.RemoveMember(_blocksDestributor.CurrentBlock.transform);
        }
    }
}
