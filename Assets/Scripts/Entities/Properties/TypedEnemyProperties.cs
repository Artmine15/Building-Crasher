using UnityEngine;

namespace Artmine15.HappyBirthday.v3.Gisha
{
    [CreateAssetMenu(fileName = "TYPE Enemy Properties", menuName = "Entities Properties/Typed Enemy")]
    public class TypedEnemyProperties : ScriptableObject
    {
        [SerializeField] private float _minMoveDeltaSpeed; public float MinMoveDeltaSpeed => _minMoveDeltaSpeed;
        [SerializeField] private float _maxMoveDeltaSpeed; public float MaxMoveDeltaSpeed => _maxMoveDeltaSpeed;

        [Space]
        [SerializeField] private Vector2 _knockoutDirection; public Vector2 KnockoutDirection => _knockoutDirection;
        [SerializeField] private float _strengthOfKnockout; public float StrengthOfKnockout => _strengthOfKnockout;
    }
}
