using Artmine15.Toolkit;
using System;
using UnityEngine;

namespace Artmine15.HappyBirthday.v3.Gisha
{
    public class EntityWarning : MonoBehaviour
    {
        [SerializeField] private float _warningTimeSeconds;
        private CommonTimer _warningTimer = new CommonTimer();
        private Vector3 _scale;

        public event Action OnWarningEnded;

        private void Update()
        {
            _warningTimer.Update(Time.deltaTime);

            if(_warningTimer.IsActive == true)
            {
                _scale.x = _warningTimer.GetNormalizedTime();
                _scale.y = _warningTimer.GetNormalizedTime();
                _scale.z = _warningTimer.GetNormalizedTime();
                transform.localScale = _scale;
            }
        }

        public void StartWarning()
        {
            _warningTimer.Start(_warningTimeSeconds);
            _warningTimer.OnEnded += EndWarning;
        }

        private void EndWarning()
        {
            _warningTimer.OnEnded -= EndWarning;
            OnWarningEnded?.Invoke();
        }
    }
}
