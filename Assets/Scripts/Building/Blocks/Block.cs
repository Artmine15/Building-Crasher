using Artmine15.Toolkit;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Artmine15.HappyBirthday.v3.Gisha
{
    public abstract class Block : MonoBehaviour
    {
        protected GlobalBuildingSystem GlobalBuildingSystem;
        protected AudioHandler AudioHandler;

        [SerializeField] private List<SpriteRenderer> _colorableSprites;
        [SerializeField] private List<Collider2D> _colliders;
        [SerializeField] private List<Light2D> _lights;
        private List<float> _lightsIntensivity = new List<float>();
        [SerializeField] private ParticleSystem _fallPSPrefab; public ParticleSystem FallPSPrefab => _fallPSPrefab;

        [Space]
        [SerializeField] private int _throwSfxPlayableChannel;

        [Space]
        [SerializeField] protected Vector2 BlockSize;

        protected StageProperties RelatedStageProperties;
        private UnifiedBlockProperties _unifiedBlockProperties => RelatedStageProperties.UnifiedBlockProperties;

        private Color[] _blockDefaultColors;
        private Color[] _blockThrowColors;
        private CommonTimer _colorTimer = new CommonTimer();
        private CommonTimer _lightsTimer = new CommonTimer();
        private CommonTimer _accelerationTimer = new CommonTimer();
        private float _currentMoveDelta;
        private CommonTimer _fallingTimer = new CommonTimer();

        private bool _isActivated; public bool IsActivated => _isActivated;
        private bool _isThrown; public bool IsThrown => _isThrown;
        protected BlockState State;

        public event Action OnActivated;
        public event Action OnThrown;
        public event Action OnFallingStarted;

        public virtual void Initialize(StageProperties stage, GlobalBuildingSystem globalBuildingSystem, AudioHandler audioHandler)
        {
            RelatedStageProperties = stage;
            GlobalBuildingSystem = globalBuildingSystem;
            AudioHandler = audioHandler;
            if (_colorableSprites.Count > 0)
                SetupColors();
            for (int i = 0; i < _lights.Count; i++)
                _lightsIntensivity.Add(_lights[i].intensity);

            State = BlockState.Untouchable;
        }

        private void Update()
        {
            _colorTimer.Update(Time.deltaTime);
            _lightsTimer.Update(Time.deltaTime);
            _accelerationTimer.Update(Time.deltaTime);
            _fallingTimer.Update(Time.deltaTime);

            switch (State)
            {
                case BlockState.Throwing:
                    for (int i = 0; i < _lights.Count; i++)
                        _lights[i].intensity = _lightsIntensivity[i] * _lightsTimer.GetNormalizedTime();

                    for (int i = 0; i < _colorableSprites.Count; i++)
                    {
                        _colorableSprites[i].color = Color.Lerp(_blockDefaultColors[i], _blockThrowColors[i],
                            _unifiedBlockProperties.NormalizedColorCurve.Evaluate(_colorTimer.GetNormalizedTime()));
                    }
                    break;
                case BlockState.Acceleration:
                    _currentMoveDelta = _unifiedBlockProperties.NormilizedAccelerationCurve.Evaluate(
                        _accelerationTimer.GetNormalizedTime()) * _unifiedBlockProperties.MoveDeltaFallingSpeed;
                    transform.Translate(Vector2.down * _currentMoveDelta * Time.deltaTime);
                    break;
                case BlockState.Falling:
                    transform.Translate(Vector2.down * _unifiedBlockProperties.MoveDeltaFallingSpeed * Time.deltaTime);
                    break;
                default:
                    break;
            }
        }

        public Vector2 GetSize() => BlockSize;

        public void MakeWaiting()
        {
            State = BlockState.Waiting;
        }

        public virtual void Activate()
        {
            if (State != BlockState.Waiting) return;

            _isActivated = true;
            OnActivated?.Invoke();
        }
        
        public virtual void Throw()
        {
            if(_isThrown == false)
            {
                foreach (var item in _colorableSprites)
                {
                    item.sortingLayerName = "Falling";
                    item.sortingOrder--;
                }
                AudioHandler.PlaySFX(_throwSfxPlayableChannel);

                _colorTimer.Start(_unifiedBlockProperties.ColorTimeSeconds, TimerGrowing.Increasing);
                _lightsTimer.Start(_unifiedBlockProperties.ColorTimeSeconds, TimerGrowing.Decreasing);
                _colorTimer.OnEnded += StartFalling;
                State = BlockState.Throwing;
                _isThrown = true;
                OnThrown?.Invoke();
            }
        }

        public virtual void StartFalling()
        {
            _colorTimer.OnEnded -= StartFalling;
            _accelerationTimer.Start(_unifiedBlockProperties.AccelerationTimeSeconds, TimerGrowing.Increasing);
            _accelerationTimer.OnEnded += SetConstantFallingSpeed;

            for (int i = 0; i < _lights.Count; i++)
                _lights[i].enabled = false;

            State = BlockState.Acceleration;
            OnFallingStarted?.Invoke();
        }

        public virtual void SetConstantFallingSpeed()
        {
            _accelerationTimer.OnEnded -= SetConstantFallingSpeed;
            _fallingTimer.Start(_unifiedBlockProperties.FallingTimeSeconds);
            _fallingTimer.OnEnded += Disable;
            State = BlockState.Falling;
        }

        private void Disable()
        {
            _fallingTimer.OnEnded -= Disable;
            gameObject.SetActive(false);
        }

        public BlockState GetState() => State;

        protected void DisableAllColliders()
        {
            for (int i = 0; i < _colliders.Count; i++)
                _colliders[i].enabled = false;
        }

        public void AddColorableSprite(SpriteRenderer spriteRenderer)
        {
            _colorableSprites.Add(spriteRenderer);
            SetupColors();
        }

        public void AddBoxCollider(Collider2D collider)
        {
            _colliders.Add(collider);
        }

        public void AddLight(Light2D light)
        {
            _lights.Add(light);
            _lightsIntensivity.Add(light.intensity);
        }

        private void SetupColors()
        {
            if (_unifiedBlockProperties == null) return;

            _blockDefaultColors = new Color[_colorableSprites.Count];
            _blockThrowColors = new Color[_colorableSprites.Count];
            float h;
            float s;
            float v;
            for (int i = 0; i < _colorableSprites.Count; i++)
            {
                _blockDefaultColors[i] = _colorableSprites[i].color;
                Color.RGBToHSV(_blockDefaultColors[i], out h, out s, out v);
                _blockThrowColors[i] = Color.HSVToRGB(h, s, v - _unifiedBlockProperties.ThrowColorValueDecrement);
            }
        }

        public void IncreaseViewsSortOrder(int increment)
        {
            foreach (var item in _colorableSprites)
            {
                item.sortingOrder += increment;
            }
        }

        public StageProperties GetStageProperties() => RelatedStageProperties;
    }
}
