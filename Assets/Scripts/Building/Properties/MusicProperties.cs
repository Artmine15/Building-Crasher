using UnityEngine;

namespace Artmine15.HappyBirthday.v3.Gisha
{
    [CreateAssetMenu(fileName = "Music Properties", menuName = "Stages Properties/Music")]
    public class MusicProperties : ScriptableObject
    {
        [SerializeField] private float _restMusicStartOffset; public float RestMusicStartOffset => _restMusicStartOffset;
        [SerializeField] private float _restMusicEndOffset; public float RestMusicEndOffset => _restMusicEndOffset;
        [SerializeField] private float _gameplayMusicStartOffset; public float GameplayMusicStartOffset => _gameplayMusicStartOffset;
        [SerializeField] private float _gameplayMusicEndOffset; public float GameplayMusicEndOffset => _gameplayMusicEndOffset;
    }
}
