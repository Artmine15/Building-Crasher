using UnityEngine;

namespace Artmine15.HappyBirthday.v3.Gisha
{
    public class SnowfallSetuper : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _mainSnowfall;
        [SerializeField] private ParticleSystem _backgroundSnowfall;
        private float _mainSnowfallLastEmission;
        private float _backgroundSnowfallLastEmission;

        private ParticleSystem.MainModule _currentMainModule;
        private ParticleSystem.EmissionModule _currentEmissionModule;
        private ParticleSystem.VelocityOverLifetimeModule _currentVelocityOverLifetimeModule;

        private void Awake()
        {
            SetCurrentModulesFromParticleSystem(ref _mainSnowfall);
            _mainSnowfallLastEmission = _currentEmissionModule.rateOverTime.constant;
            SetCurrentModulesFromParticleSystem(ref _backgroundSnowfall);
            _backgroundSnowfallLastEmission = _currentEmissionModule.rateOverTime.constant;
        }

        public void SetNewSnowfall(SnowfallProperties properties)
        {
            SetNewSnowfall(properties.MainSnowfallSettings, properties, ref _mainSnowfall, ref _mainSnowfallLastEmission);
            SetNewSnowfall(properties.BackgroundSnowfallSettings, properties, ref _backgroundSnowfall, ref _backgroundSnowfallLastEmission);
        }

        private void SetNewSnowfall(SnowfallSettings settings, SnowfallProperties properties, ref ParticleSystem particleSystem, ref float lastEmission)
        {
            SetCurrentModulesFromParticleSystem(ref particleSystem);
            lastEmission = settings.Emission;

            _currentMainModule.startLifetime = settings.LifetimeSeconds;
            _currentMainModule.startSpeed = settings.Speed;
            _currentEmissionModule.rateOverTime = lastEmission / 3;

            if (properties.IsOverrideVelocityOverLifetime == false) return;

            _currentVelocityOverLifetimeModule.orbitalX = properties.OrbitalXVelocity;
            _currentVelocityOverLifetimeModule.orbitalY = properties.OrbitalYVelocity;
            _currentVelocityOverLifetimeModule.orbitalZ = properties.OrbitalZVelocity;
        }

        public void ApplyNewSnowfall()
        {
            SetCurrentModulesFromParticleSystem(ref _mainSnowfall);
            _currentEmissionModule.rateOverTime = _mainSnowfallLastEmission;
            SetCurrentModulesFromParticleSystem(ref _backgroundSnowfall);
            _currentEmissionModule.rateOverTime = _backgroundSnowfallLastEmission;
        }

        private void SetCurrentModulesFromParticleSystem(ref ParticleSystem particleSystem)
        {
            _currentMainModule = particleSystem.main;
            _currentEmissionModule = particleSystem.emission;
            _currentVelocityOverLifetimeModule = particleSystem.velocityOverLifetime;
        }
    }
}