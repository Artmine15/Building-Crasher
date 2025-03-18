using Artmine15.SceneTransitionSystem.Components;
using UnityEngine;
using VContainer;

namespace Artmine15.HappyBirthday.v3.Gisha
{
    public class CreditsActivator : MonoBehaviour
    {
        [Inject] private InputReceiver _inputReceiver;
        [Inject] private AudioHandler _audioHandler;

        [SerializeField] private SceneTransitionIn _transitionIn;
        [SerializeField] private Animator _credits;
        [SerializeField] private GameObject[] _objectsToDisableOnCredits;

        [Space]
        [SerializeField] private float _startMusicOffset;
        [SerializeField] private float _endMusicOffset;

        private bool _isCreditsStarted;

        private void Awake()
        {
            _isCreditsStarted = false;
        }

        public void StartCredits()
        {
            if (_isCreditsStarted == true) return;

            _inputReceiver.DisableInput();
            _audioHandler.PlayNewMusicOffset(_startMusicOffset, _endMusicOffset);
            _transitionIn.StartTransition();
            _transitionIn.OnTransitionEnded += EnableCredits;
            _isCreditsStarted = true;
        }

        private void EnableCredits()
        {
            _transitionIn.OnTransitionEnded -= EnableCredits;
            foreach (var item in _objectsToDisableOnCredits)
                item.SetActive(false);
            
            _credits.gameObject.SetActive(true);
            _credits.SetTrigger("Start");
        }
    }
}
