using NTC.Pool;
using UnityEngine;

namespace Artmine15.HappyBirthday.v3.Gisha
{
    public abstract class Entity : MonoBehaviour, IPoolable
    {
        protected EntitiesContainer ThisEntitiesContainer;
        protected ParticlesSpawner ThisParticlesSpawner;

        [SerializeField] private EntityWarning _warningObject;
        [SerializeField] protected Collider2D EntityCollider;
        [SerializeField] private GameObject _viewContainer;

        [Space]
        [SerializeField] protected Vector2 Size; public Vector2 ObjectSize => Size;

        [Space]
        [SerializeField] protected ParticleSystem OnDespawnParticleSystemPrefab;

        protected Block RelatedBlock;

        protected bool IsEntityEnabled;

        public virtual void Initialize(EntitiesContainer entitiesContainer, ParticlesSpawner particlesSpawner)
        {
            ThisEntitiesContainer = entitiesContainer;
            ThisParticlesSpawner = particlesSpawner;
            SetDefault();
        }

        public virtual void OnSpawn()
        {
            _warningObject.gameObject.SetActive(true);
            _warningObject.StartWarning();
            _warningObject.OnWarningEnded += EnableEntity;
        }

        public virtual void SetupAfterSpawn(Block relatedBlock)
        {
            RelatedBlock = relatedBlock;
        }

        public virtual void OnDespawn()
        {
            if (OnDespawnParticleSystemPrefab != null)
                ThisParticlesSpawner.PlayParticleSystemOnce(OnDespawnParticleSystemPrefab, transform.position);
            SetDefault();
        }

        private void SetDefault()
        {
            _warningObject.gameObject.SetActive(false);
            _viewContainer.SetActive(false);
            EntityCollider.enabled = false;
            IsEntityEnabled = false;
        }

        protected virtual void EnableEntity()
        {
            _warningObject.OnWarningEnded -= EnableEntity;
            _warningObject.gameObject.SetActive(false);
            _viewContainer.SetActive(true);
            EntityCollider.enabled = true;
            IsEntityEnabled = true;
        }
    }
}
