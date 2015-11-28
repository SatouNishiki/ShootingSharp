using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.position;
using ShootingSharp.interfaces;

namespace ShootingSharp.entity.shot
{
    public abstract class CircleShot : Shot
    {
        /*
        public CircleShot(Builder builder)
            : base(builder)
        {

        }*/

        public CircleShot()
            : base()
        {

        }

        protected override void Init()
        {
            base.Init();

            collid.CircleCollider c = new collid.CircleCollider(this.GetType(), null);
            foreach (var n in noCollitionTypes)
            {
                c.AddNoCollitionTypes(n);
            }
            c.Radius = this.GetRadius();
            this.collider = c;
        }

        public abstract int GetRadius();
    }
}
