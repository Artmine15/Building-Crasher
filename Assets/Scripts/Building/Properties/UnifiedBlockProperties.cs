using UnityEngine;

namespace Artmine15.HappyBirthday.v3.Gisha
{
    [CreateAssetMenu(fileName = "Unified Block Properties", menuName = "Stages Properties/Unified/Block")]
    public class UnifiedBlockProperties : ScriptableObject
    {
        [Space]
        [SerializeField] private AnimationCurve _normalizedColorCurve; public AnimationCurve NormalizedColorCurve => _normalizedColorCurve;
        [SerializeField] private float _colorTimeSeconds = 1; public float ColorTimeSeconds => _colorTimeSeconds;

        [SerializeField] private float _throwColorValue = 0.5f; public float ThrowColorValueDecrement => _throwColorValue;

        [Space]
        [SerializeField] private AnimationCurve _normilizedAccelerationCurve; public AnimationCurve NormilizedAccelerationCurve => _normalizedColorCurve;
        [SerializeField] private float _accelerationTimeSeconds = 1; public float AccelerationTimeSeconds => _accelerationTimeSeconds;

        [Space]
        [SerializeField] private float _moveDeltaFallingSpeed; public float MoveDeltaFallingSpeed => _moveDeltaFallingSpeed;
        [SerializeField] private float _fallingTimeSeconds = 1; public float FallingTimeSeconds => _fallingTimeSeconds;
    }
}
