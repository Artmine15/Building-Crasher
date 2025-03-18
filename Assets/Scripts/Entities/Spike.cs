using UnityEngine;

namespace Artmine15.HappyBirthday.v3.Gisha
{
    public class Spike : Entity
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite[] _randomViewSprites;

        public override void Initialize(EntitiesContainer entitiesContainer, ParticlesSpawner particlesSpawner)
        {
            base.Initialize(entitiesContainer, particlesSpawner);
            _spriteRenderer.sprite = _randomViewSprites[Random.Range(0, _randomViewSprites.Length)];
        }
    }
}
