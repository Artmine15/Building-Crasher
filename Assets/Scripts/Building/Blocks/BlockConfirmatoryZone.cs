using UnityEngine;

namespace Artmine15.HappyBirthday.v3.Gisha
{
    public class BlockConfirmatoryZone : MonoBehaviour
    {
        [SerializeField] private Block _relatedBlock; public Block RelatedBlock => _relatedBlock;
        [SerializeField] private BlockConfirmationCondition _confirmationCondition; public BlockConfirmationCondition ConfirmationCondition => _confirmationCondition;

        public bool IsConfirmed { get; private set; }
        public void Confirm() => IsConfirmed = true;
    }
}
