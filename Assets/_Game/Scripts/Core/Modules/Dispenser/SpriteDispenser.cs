using UnityEngine;

namespace Core.Modules.Dispenser
{
    public class SpriteDispenser : RandomDispenser<Sprite>
    {
        public SpriteDispenser(Sprite[] setOfSprites) : base(setOfSprites)
        { }
    }
}
