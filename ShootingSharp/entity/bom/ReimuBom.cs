using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootingSharp.entity.bom
{
    public class ReimuBom : Bom
    {
        public override string GetTextureName()
        {
            return "reimu_bom.png";
        }

        public override System.Drawing.Size GetTextureSize()
        {
            return new System.Drawing.Size(30, 30);
        }
    }
}
