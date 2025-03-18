using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Artmine15.HappyBirthday.v3.Gisha
{
    public class BrokenBlockPart : MonoBehaviour
    {
        public Block RelatedBlock { get; private set; }
        [SerializeField] private SpriteRenderer _spriteRenderer; public SpriteRenderer SpriteRenderer => _spriteRenderer;
        [SerializeField] private Collider2D[] _colliders; public Collider2D[] Colliders => _colliders;
        [SerializeField] private Light2D[] _lights; public Light2D[] Lights => _lights;

        public void Initialize(Block block)
        {
            RelatedBlock = block;
        }
    }
}
