using Artmine15.Toolkit;
using System;
using UnityEngine;

namespace Artmine15.HappyBirthday.v3.Gisha
{
    public class EntityWarning : MonoBehaviour
    {
        [SerializeField] private float _warningTimeSeconds;
        private Timer _warningTimer = new Timer();
        private Vector3 _scale;

        public event Action OnWarningEnded;

        private void Update()
        {
            _warningTimer.UpdateTimer(Time.deltaTime);

            if(_warningTimer.GetCurrentTimerType() != TimerType.None)
            {
                _scale.x = _warningTimer.GetTimerNormalizedValue();
                _scale.y = _warningTimer.GetTimerNormalizedValue();
                _scale.z = _warningTimer.GetTimerNormalizedValue();
                transform.localScale = _scale;
            }
        }

        public void StartWarning()
        {
            _warningTimer.StartTimer(_warningTimeSeconds, TimerType.Common);
            _warningTimer.OnTimerEnded += EndWarning;
        }

        private void EndWarning()
        {
            _warningTimer.OnTimerEnded -= EndWarning;
            OnWarningEnded?.Invoke();
        }
    }
}
