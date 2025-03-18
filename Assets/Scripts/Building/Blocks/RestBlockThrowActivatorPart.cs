using UnityEngine;

namespace Artmine15.HappyBirthday.v3.Gisha
{
    public class RestBlockThrowActivatorPart : MonoBehaviour
    {
        [SerializeField] private RestBlock _thisRestBlock;
        public bool IsBlockActivated => _thisRestBlock.IsActivated;
        public bool IsBlockThrown => _thisRestBlock.IsThrown;

        public void ThrowBlock()
        {
            _thisRestBlock.Throw();
        }
    }
}
