using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.interfaces;

namespace ShootingSharp.entity.shot
{
    public class BlueFairyShot : EnemyCircleShot
    {
        public enum ColorType
        {
            Normal, Dark
        }

        public ColorType Color { get; set; }

        public BlueFairyShot(IHasSSPosition shooter) : base(shooter) 
        {
            this.MoveSpeed = 3;
        }

        public BlueFairyShot(IHasSSPosition shooter, double theta)
            : base(shooter, theta)
        {
            this.MoveSpeed = 3;
        }

        public BlueFairyShot(IHasSSPosition shooter, position.SSPosition target)
            : base(shooter, target)
        {
            this.MoveSpeed = 3;
        }


        public override string GetTextureName()
        {
            if (Color == ColorType.Dark)
                return "dark_blue_fairy_shot.png";
            else
                return "blue_fairy_shot.png";
        }

        public override System.Drawing.Size GetTextureSize()
        {
            return new System.Drawing.Size(20, 20);
        }

        public override int GetRadius()
        {
            return 3;
        }

    }
}
