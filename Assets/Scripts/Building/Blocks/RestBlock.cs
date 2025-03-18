
namespace Artmine15.HappyBirthday.v3.Gisha
{
    public class RestBlock : Block
    {
        public override void StartFalling()
        {
            base.StartFalling();
            DisableAllColliders();
        }
    }
}
