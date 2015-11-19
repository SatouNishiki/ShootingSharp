using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShootingSharp.interfaces;

namespace ShootingSharp.entity.shot
{
    public class BlueFairyShot : Shot
    {
        public enum ColorType
        {
            Normal, Dark
        }

        public ColorType Color { get; set; }

        public BlueFairyShot(IHasSSPosition shooter) : base(shooter) 
        {
            this.moveSpeed = 3;
        }

        public BlueFairyShot(IHasSSPosition shooter, double theta)
            : base(shooter, theta)
        {
            this.moveSpeed = 3;
        }

        public BlueFairyShot(IHasSSPosition shooter, position.SSPosition target)
            : base(shooter, target)
        {
            this.moveSpeed = 3;
        }

        public override void OnDeath()
        {
            //なにもしない
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

        public override void DoAction()
        {
        }

        public override int GetRadius()
        {
            return 3;
        }

        public override position.SquareSSPositon GetSquarePosition()
        {
            throw new NotImplementedException();
        }

        public override interfaces.SharpType GetSharpType()
        {
            return SharpType.Circle;
        }
    }
}
