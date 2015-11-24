using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.entity.shot;
using ShootingSharp.interfaces;
using ShootingSharp.position;
using DxLibDLL;

namespace DebugProject.shot
{
    public class CirclesShot : EnemyCircleShot
    {
        private int colorType;


        public CirclesShot(IHasSSPosition shooter, double theta, int color)
            : base(shooter, theta)
        {
            this.MoveSpeed = 3;
            this.colorType = color;
        }

        public CirclesShot(IHasSSPosition shooter, SSPosition target, int color)
            : base(shooter, target)
        {
            this.MoveSpeed = 3;
            this.colorType = color;
        }

        public CirclesShot(IHasSSPosition shooter, IHasSSPosition target, int color)
            : base(shooter, target)
        {
            this.MoveSpeed = 3;
            this.colorType = color;
        }


        public override int GetRadius()
        {
            return 10;
        }

        public override string GetTextureName()
        {
            if (this.colorType < 3)
            {
                return "circles" + this.colorType.ToString() + ".png";
            }
            else
            {
                return "circles2.png";
            }
        }

        public override System.Drawing.Size GetTextureSize()
        {
            return new System.Drawing.Size(30, 30);
        }


    }
}
