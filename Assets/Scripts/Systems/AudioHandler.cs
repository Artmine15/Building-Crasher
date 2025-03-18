using Artmine15.Toolkit;
using UnityEngine;

namespace Artmine15.HappyBirthday.v3.Gisha
{
    public class AudioHandler : MonoBehaviour
    {
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _sfxSource;
        [SerializeField] private AudioSource[] _playableSfxSources;

        [Space]
        [SerializeField] private float _musicFadeInSeconds;
        [SerializeField] private float _musicFadeOutSeconds;
        private Timer _fadeTimer = new Timer();
        private MusicState _musicState = MusicState.Stopped;

        private float _volume;

        private float _startOffset;
        private float _endOffset;

        private void Update()
        {
            _fadeTimer.UpdateTimer(Time.deltaTime);

            switch (_musicState)
            {
                case MusicState.Stopped:
                    break;
                case MusicState.FadingIn:
                    _musicSource.volume = _fadeTimer.GetTimerNormalizedValue();
                    break;
                case MusicState.Playing:
                    if (_musicSource.time >= _endOffset - _musicFadeOutSeconds)
                    {
                        RepeatMusicFromStart();
                    }
                    break;
                case MusicState.FadingOut:
                    _musicSource.volume = _volume * _fadeTimer.GetTimerNormalizedValue();
                    break;
                default:
                    break;
            }
        }

        public void PlayNewMusicOffset(float start, float end)
        {
            _startOffset = start;
            _endOffset = end;
            RepeatMusicFromStart();
        }

        private void RepeatMusicFromStart()
        {
            if(_musicState == MusicState.Playing)
            {
                FadeOutMusic();
                _fadeTimer.OnTimerEnded += FadeInMusic;
            }
            else
            {
                FadeInMusic();
            }
        }

        private void FadeInMusic()
        {
            _musicSource.time = _startOffset;
            _volume = 0;
            _fadeTimer.StartTimer(_musicFadeInSeconds, TimerType.Common, TimerGrowing.Increasing);
            _fadeTimer.OnTimerEnded -= FadeInMusic;
            _fadeTimer.OnTimerEnded += MakePlaying;
            _musicState = MusicState.FadingIn;
        }

        private void FadeOutMusic()
        {
            _volume = _musicSource.volume;
            _fadeTimer.StartTimer(_musicFadeOutSeconds, TimerType.Common, TimerGrowing.Decreasing);
            _musicState = MusicState.FadingOut;
        }

        private void MakePlaying()
        {
            _fadeTimer.OnTimerEnded -= MakePlaying;
            _musicState = MusicState.Playing;
        }

        public void PlaySFX(AudioClip clip)
        {
            _sfxSource.PlayOneShot(clip);
        }

        public void PlayRandomSFX(AudioClip[] clips)
        {
            PlaySFX(clips[Random.Range(0, clips.Length)]);
        }

        public void PlaySFX(int playableChannel)
        {
            _playableSfxSources[playableChannel].Play();
        }
    }
}
