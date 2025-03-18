using UnityEngine;

namespace Artmine15.HappyBirthday.v3.Gisha
{
    [CreateAssetMenu(fileName ="Broken Block Properties", menuName = "Stages Properties/Brocken Block")]
    public class BrokenBlockProperties : ScriptableObject
    {
        [SerializeField] private BrokenBlockPart[] _parts; public BrokenBlockPart[] Parts => _parts;
    }
}
