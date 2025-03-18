using UnityEngine;

namespace Artmine15.HappyBirthday.v3.Gisha
{
    [CreateAssetMenu(fileName = "Unified Enemy Properties", menuName = "Entities Properties/Unified Enemy")]
    public class UnifiedEnemyProperties : ScriptableObject
    {
        [SerializeField] private float _knockoutTime; public float KnockoutTime => _knockoutTime;
        [SerializeField] private float _fallingYDistance; public float FallingYDistance => _fallingYDistance;
    }
}
