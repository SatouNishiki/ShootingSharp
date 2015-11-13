using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace ShootingSharp.entity.shot
{
    public class NormalShot : Shot
    {
        public NormalShot(interfaces.IHasSSPosition shooter)
            :base(shooter)
        {
            this.deleteTime = 60;
        }

        public override void DoAction()
        {
            throw new NotImplementedException();
        }

        public override int GetRadius()
        {
            return 5;
        }

        public override bool IsInteract(interfaces.IInteracter obj)
        {
            //throw new NotImplementedException();
            return false;
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
