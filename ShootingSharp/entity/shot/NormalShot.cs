using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;
using System.Drawing;
using ShootingSharp.interfaces;

namespace ShootingSharp.entity.shot
{
    public class NormalShot : CircleShot
    {

        public NormalShot(IHasSSPosition shooter) : base(shooter) { }

        public NormalShot(IHasSSPosition shooter, double theta) : base(shooter, theta) { }


        public override int GetRadius()
        {
            return 5;
        }

        public override string GetTextureName()
        {
            throw new NotImplementedException();
        }

        public override System.Drawing.Size GetTextureSize()
        {
            throw new NotImplementedException();
        }

        public override position.SSPosition GetTexturePosition()
        {
            throw new NotImplementedException();
        }

        public override void Draw()
        {
            DX.DrawCircle(this.position.PosX, this.position.PosY, this.GetRadius(), DX.GetColor(0, 255, 0));
        }

    }
}
