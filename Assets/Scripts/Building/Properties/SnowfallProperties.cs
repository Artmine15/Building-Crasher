using TriInspector;
using UnityEngine;

namespace Artmine15.HappyBirthday.v3.Gisha
{
    [CreateAssetMenu(fileName = "Snowfall Properties", menuName = "Stages Properties/Snowfall")]
    public class SnowfallProperties : ScriptableObject
    {
        [SerializeField] private SnowfallSettings _mainSnowfallSettings; public SnowfallSettings MainSnowfallSettings => _mainSnowfallSettings;
        [SerializeField] private SnowfallSettings _backgroundSnowfallSettings; public SnowfallSettings BackgroundSnowfallSettings => _backgroundSnowfallSettings;

        [Space]
        [SerializeField] private bool _isOverrideVelocityOverLifetime; public bool IsOverrideVelocityOverLifetime => _isOverrideVelocityOverLifetime;
        [SerializeField, DisableIf(nameof(_isOverrideVelocityOverLifetime), false)] private ParticleSystem.MinMaxCurve _orbitalXVelocity; public ParticleSystem.MinMaxCurve OrbitalXVelocity => _orbitalXVelocity;
        [SerializeField, DisableIf(nameof(_isOverrideVelocityOverLifetime), false)] private ParticleSystem.MinMaxCurve _orbitalYVelocity; public ParticleSystem.MinMaxCurve OrbitalYVelocity => _orbitalYVelocity;
        [SerializeField, DisableIf(nameof(_isOverrideVelocityOverLifetime), false)] private ParticleSystem.MinMaxCurve _orbitalZVelocity; public ParticleSystem.MinMaxCurve OrbitalZVelocity => _orbitalZVelocity;
    }
}

