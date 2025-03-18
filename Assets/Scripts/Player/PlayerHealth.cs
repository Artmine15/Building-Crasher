using Artmine15.Toolkit;
using System;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;

namespace Artmine15.HappyBirthday.v3.Gisha
{
    public class PlayerHealth : MonoBehaviour
    {
        [Inject] private AudioHandler _audioHandler;

        [SerializeField] private UIDocument _gameUIDocument;

        [Space]
        [SerializeField] private int _healthAmount;
        private int _currentHealth;
        [SerializeField] private int CurrentHealthDisplay => _currentHealth;

        [Space]
        [SerializeField] private float _cooldownTimeSeconds = 1;
        [SerializeField] private float _blinkFrequency = 2;
        private Timer _cooldownTimer = new Timer();
        private Timer _cooldownViewTimer = new Timer();


        [Space]
        [SerializeField] private SpriteRenderer[] _views;
        private bool _isViewEnabled;

        [Space]
        [SerializeField] private AudioClip _damageClip;

        public event Action OnHealthIsOver;

        private void Awake()
        {
            _currentHealth = _healthAmount;
            _gameUIDocument.rootVisualElement.Q<Label>("HealthDisplay").dataSource = this;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (IsPlayerTouchComponent(collision.collider) == true)
            {
                ApplyDamage();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (IsPlayerTouchComponent(collision) == true)
            {
                ApplyDamage();
            }
        }

        private void Update()
        {
            _cooldownTimer.UpdateTimer(Time.deltaTime);
            _cooldownViewTimer.UpdateTimer(Time.deltaTime);
        }

        private void ApplyDamage()
        {
            if (_cooldownTimer.GetTimerValue() > 0) return;

            if(_currentHealth > 0)
            {
                _currentHealth--;
                ApplyCooldown();
                _audioHandler.PlaySFX(_damageClip);
                if (_currentHealth <= 0)
                {
                    OnHealthIsOver?.Invoke();
                }
            }
            else
            {
                OnHealthIsOver?.Invoke();
            }
        }

        public void ApplyCooldown()
        {
            if (_cooldownTimer.GetTimerValue() > 0) return;

            _cooldownTimer.StartTimer(_cooldownTimeSeconds, TimerType.Common);
            _cooldownViewTimer.StartTimer(_cooldownTimeSeconds / _blinkFrequency, TimerType.Repeatable);
            _isViewEnabled = true;

            _cooldownTimer.OnTimerEnded += EndCooldown;
            _cooldownViewTimer.OnTimerRepeated += SwitchView;
        }

        private void EndCooldown()
        {
            _cooldownTimer.OnTimerEnded -= EndCooldown;
            _cooldownViewTimer.OnTimerRepeated -= SwitchView;
            _cooldownViewTimer.Stop();
            //SwitchView();
            EnableView();
        }

        private bool IsPlayerTouchComponent(Collider2D collider)
        {
            if (collider.GetComponent<Enemy>() == true ||
                collider.GetComponent<Spike>() == true ||
                collider.GetComponent<BrokenBlockPart>() == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void SwitchView()
        {
            _isViewEnabled = !_isViewEnabled;
            foreach (var item in _views)
            {
                item.color = new Color(item.color.r, item.color.g, item.color.b, _isViewEnabled ? 1 : 0);
            }
        }

        private void EnableView()
        {
            foreach (var item in _views)
            {
                item.color = new Color(item.color.r, item.color.g, item.color.b, 1);
            }
        }
    }
}
