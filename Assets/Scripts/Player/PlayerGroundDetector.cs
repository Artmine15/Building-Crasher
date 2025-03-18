using System;
using UnityEngine;
using VContainer;

namespace Artmine15.HappyBirthday.v3.Gisha
{
    public class PlayerGroundDetector : MonoBehaviour
    {
        [Inject] private PlayerStates _playerStates;

        [SerializeField] private Vector2 _rayOffset;
        [SerializeField] private float _rayLenght;
        [SerializeField] private LayerMask _mask;

        private int _hitsCount;
        private RaycastHit2D[] _hits = new RaycastHit2D[1];

        private bool _isPhysicallyOnGround;

        private void Update()
        {
            _hitsCount = Physics2D.RaycastNonAlloc((Vector2)transform.position + _rayOffset, Vector2.down, _hits, _rayLenght, _mask);
            if (_hitsCount > 0 && _isPhysicallyOnGround == true)
                _playerStates.IsOnGround = true;
            else
                _playerStates.IsOnGround = false;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.GetComponent<Block>() == true ||
                collision.collider.GetComponent<BrokenBlockPart>() == true)
            {
                _isPhysicallyOnGround = true;
            }
        }
        //very bad system(
        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.collider.TryGetComponent(out Block block) == true)
                CheckAllDisagreements(block);

            if (collision.collider.TryGetComponent(out BrokenBlockPart brokenBlockPart) == true)
                CheckAllDisagreements(brokenBlockPart.RelatedBlock);
        }

        private void CheckAllDisagreements(Block touchedBlock)
        {
            CheckBlockDisagreement(touchedBlock, _hits[0].collider.TryGetComponent(out Block blockBelow), blockBelow);
            CheckBlockDisagreement(touchedBlock, _hits[0].collider.TryGetComponent(out BrokenBlockPart brokenBlockPartBelow), brokenBlockPartBelow ? brokenBlockPartBelow.RelatedBlock : blockBelow);
        }

        private void CheckBlockDisagreement(Block touchedBlock, bool isBlock, Block blockBelow)
        {
            if (touchedBlock.GetState() != BlockState.Untouchable)
            {
                if (_hitsCount > 0)
                {
                    if (isBlock == true)
                    {
                        if (blockBelow.GetState() == BlockState.Untouchable)
                            _isPhysicallyOnGround = false;
                    }
                }
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.collider.GetComponent<Block>() == true ||
                collision.collider.GetComponent<BrokenBlockPart>() == true)
                _isPhysicallyOnGround = false;
        }
    }
}

