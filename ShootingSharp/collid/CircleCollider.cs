using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.position;

namespace ShootingSharp.collid
{
    public class CircleCollider : ColliderBase
    {
        public CircleCollider(Type baseType, params Type[] noCollitionTypes)
            : base(baseType, noCollitionTypes)
        {

        }

        public int Radius { get; set; }

   //     public SSPosition CenterPosition { get; set; }

        public override ColliderBase.SharpType GetSharpType()
        {
            return SharpType.Circle;
        }
    }
}
