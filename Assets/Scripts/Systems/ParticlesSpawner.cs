using NTC.Pool;
using UnityEngine;

namespace Artmine15.HappyBirthday.v3.Gisha
{
    public class ParticlesSpawner
    {
        private ParticleSystem _lastSpawnedPS;

        public void PlayParticleSystemOnce(ParticleSystem prefab)
        {
            _lastSpawnedPS = NightPool.Spawn(prefab.gameObject).GetComponent<ParticleSystem>();
            _lastSpawnedPS.Play();
            NightPool.Despawn(_lastSpawnedPS.gameObject, _lastSpawnedPS.main.startLifetime.constant);
        }

        public void PlayParticleSystemOnce(ParticleSystem prefab, Vector2 position)
        {
            PlayParticleSystemOnce(prefab);
            _lastSpawnedPS.transform.position = position;
        }

        public void PlayParticleSystemOnce(ParticleSystem prefab, Vector2 position, Quaternion rotation)
        {
            PlayParticleSystemOnce(prefab, position);
            _lastSpawnedPS.transform.rotation = rotation;
        }

        public void PlayParticleSystemOnce(ParticleSystem prefab, Vector2 position, Vector2 localScale)
        {
            PlayParticleSystemOnce(prefab, position);
            _lastSpawnedPS.transform.localScale = localScale;
        }

        public void PlayParticleSystemOnce(ParticleSystem prefab, Vector2 position, Quaternion rotation, Vector2 localScale)
        {
            PlayParticleSystemOnce(prefab, position, rotation);
            _lastSpawnedPS.transform.localScale = localScale;
        }
    }
}
