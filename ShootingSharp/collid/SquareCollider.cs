using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.position;

namespace ShootingSharp.collid
{
    public class SquareCollider : ColliderBase
    {
        public SquareCollider(Type baseType, params Type[] noCollitionTypes)
            : base(baseType, noCollitionTypes)
        {

        }

        public SquareSSPositon SquarePosition { get; set; }

        public override ColliderBase.SharpType GetSharpType()
        {
            return SharpType.Square;
        }
    }
}
