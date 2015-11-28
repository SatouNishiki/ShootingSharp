using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.position;
using ShootingSharp.interfaces;

namespace ShootingSharp.entity.shot
{
    public abstract class PlayerCircleShot : CircleShot
    {
        /*
        public PlayerCircleShot(Builder builder)
            : base(builder)
        {

        }*/
        public PlayerCircleShot()
            : base()
        {

        }

        protected override void Init()
        {
            base.Init();
            this.collider.AddNoCollitionTypes(typeof(entity.EntityPlayer), typeof(bom.Bom));
        }
    }
}
