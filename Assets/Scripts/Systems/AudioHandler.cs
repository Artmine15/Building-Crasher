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
        private CommonTimer _fadeTimer = new CommonTimer();
        private MusicState _musicState = MusicState.Stopped;

        private float _volume;

        private float _startOffset;
        private float _endOffset;

        private void Update()
        {
            _fadeTimer.Update(Time.deltaTime);

            switch (_musicState)
            {
                case MusicState.Stopped:
                    break;
                case MusicState.FadingIn:
                    _musicSource.volume = _fadeTimer.GetNormalizedTime();
                    break;
                case MusicState.Playing:
                    if (_musicSource.time >= _endOffset - _musicFadeOutSeconds)
                    {
                        RepeatMusicFromStart();
                    }
                    break;
                case MusicState.FadingOut:
                    _musicSource.volume = _volume * _fadeTimer.GetNormalizedTime();
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
                _fadeTimer.OnEnded += FadeInMusic;
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
            _fadeTimer.Start(_musicFadeInSeconds, TimerGrowing.Increasing);
            _fadeTimer.OnEnded -= FadeInMusic;
            _fadeTimer.OnEnded += MakePlaying;
            _musicState = MusicState.FadingIn;
        }

        private void FadeOutMusic()
        {
            _volume = _musicSource.volume;
            _fadeTimer.Start(_musicFadeOutSeconds, TimerGrowing.Decreasing);
            _musicState = MusicState.FadingOut;
        }

        private void MakePlaying()
        {
            _fadeTimer.OnEnded -= MakePlaying;
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
