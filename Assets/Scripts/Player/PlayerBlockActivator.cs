using System;
using UnityEngine;
using VContainer;

namespace Artmine15.HappyBirthday.v3.Gisha
{
    public class PlayerBlockActivator : MonoBehaviour
    {
        [Inject] private InputReceiver _inputReceiver;
        [Inject] private StagesSystem _stagesSystem;
        [Inject] private ParticlesSpawner _particlesSpawner;

        [SerializeField] private ParticleSystem _restBlockActivationParticleSystem;

        private bool _isCanActivateBlocks;
        private bool _isCanActivateRestBlock;
        private RestBlockThrowActivatorPart _restBlockThrowActivator;

        private bool _isCanActivateCredits;
        private CreditsActivator _creditsActivator;

        private Block _lastTouchedBlock; public Block LastTouchedBlock => _lastTouchedBlock;

        public event Action OnNewBlockTouched;
        public event Action OnLastTouchedBlockThrown;

        public event Action OnPlayerTouchedBlockWhenCannotActivate;

        private void OnEnable()
        {
            _inputReceiver.OnAttackPerformed += TryActivateRestBlock;
            _inputReceiver.OnAttackPerformed += TryActivateCredits;
        }

        private void OnDisable()
        {
            _inputReceiver.OnAttackPerformed -= TryActivateRestBlock;
            _inputReceiver.OnAttackPerformed -= TryActivateCredits;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            TryConfirmBlock(collision.collider);

            TryActivateBlock(collision.collider);
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            UnsubscribeBlockConfirmation(collision.collider);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            TryConfirmBlock(collision);
            
            TryActivateBlock(collision);

            if (collision.TryGetComponent(out RestBlockThrowActivatorPart activator) == true)
            {
                _restBlockThrowActivator = activator;
                _isCanActivateRestBlock = true;
            }

            if (collision.TryGetComponent(out CreditsActivator creditsActivator) == true)
            {
                _creditsActivator = creditsActivator;
                _isCanActivateCredits = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            UnsubscribeBlockConfirmation(collision);

            if (collision.GetComponent<RestBlockThrowActivatorPart>() == true)
            {
                _restBlockThrowActivator = null;
                _isCanActivateRestBlock = false;
            }

            if (collision.GetComponent<CreditsActivator>() == true)
            {
                _creditsActivator = null;
                _isCanActivateCredits = false;
            }
        }

        private void TryConfirmBlock(Collider2D collider)
        {
            if (collider.TryGetComponent(out BlockConfirmatoryZone blockConfirmatoryZone) == true)
            {
                if (blockConfirmatoryZone.IsConfirmed == true) return;

                switch (blockConfirmatoryZone.ConfirmationCondition)
                {
                    case BlockConfirmationCondition.OnActivation:
                        _isCanActivateBlocks = true;
                        break;
                    case BlockConfirmationCondition.OnThrown:
                        _isCanActivateBlocks = true;
                        blockConfirmatoryZone.RelatedBlock.OnThrown += () =>
                        {
                            _isCanActivateBlocks = true;
                        };
                        break;
                    default:
                        break;
                }
                blockConfirmatoryZone.Confirm();
            }
        }

        private void UnsubscribeBlockConfirmation(Collider2D collider)
        {
            if (collider.TryGetComponent(out BlockConfirmatoryZone blockConfirmatoryZone) == true)
            {
                switch (blockConfirmatoryZone.ConfirmationCondition)
                {
                    case BlockConfirmationCondition.OnThrown:
                        blockConfirmatoryZone.RelatedBlock.OnThrown -= () =>
                        {
                            _isCanActivateBlocks = true;
                        };
                        break;
                }
            }
        }

        private void TryActivateBlock(Collider2D collider)
        {
            if (collider.TryGetComponent(out Block block) == true)
            {
                if (block.IsActivated == true) return;

                if (_isCanActivateBlocks == false || block.GetState() == BlockState.Untouchable)
                {
                    OnPlayerTouchedBlockWhenCannotActivate?.Invoke();
                }

                if (_isCanActivateBlocks == true)
                {
                    HandleLastBlock(block);
                    OnNewBlockTouched?.Invoke();
                    block.Activate();

                    if (block is RestBlock)
                    {
                        _stagesSystem.Rest(block.GetStageProperties());
                    }
                    _isCanActivateBlocks = false;
                }
            }

            if(collider.TryGetComponent(out BrokenBlockPart brokenBlockPart) == true)
            {
                if (_isCanActivateBlocks == false || brokenBlockPart.RelatedBlock.GetState() == BlockState.Untouchable)
                {
                    OnPlayerTouchedBlockWhenCannotActivate?.Invoke();
                }
            }
        }

        private void HandleLastBlock(Block block)
        {
            if (_lastTouchedBlock != null)
                _lastTouchedBlock.OnThrown -= () => OnLastTouchedBlockThrown?.Invoke();
            _lastTouchedBlock = block;
            _lastTouchedBlock.OnThrown += () => OnLastTouchedBlockThrown?.Invoke();
        }

        private void TryActivateRestBlock()
        {
            if (_isCanActivateRestBlock == false || _restBlockThrowActivator.IsBlockThrown == true || _restBlockThrowActivator.IsBlockActivated == false) return;

            _particlesSpawner.PlayParticleSystemOnce(_restBlockActivationParticleSystem, _restBlockThrowActivator.transform.position);
            _restBlockThrowActivator.ThrowBlock();
            _stagesSystem.MoveToNextStage();
        }

        private void TryActivateCredits()
        {
            if (_isCanActivateCredits == false) return;

            _creditsActivator.StartCredits();
        }
    }
} 
